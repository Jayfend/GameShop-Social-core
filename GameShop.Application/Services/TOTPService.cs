using AspNetCore.Totp;
using AspNetCore.Totp.Interface;
using AspNetCore.Totp.Interface.Models;
using GameShop.Data.Entities;
using GameShop.Utilities.Exceptions;
using GameShop.ViewModels.System.Users;
using Microsoft.AspNetCore.Identity;
using OtpNet;
using QRCoder;
using System;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using Nest;
using System.Drawing.Imaging;
using System.IO;

namespace GameShop.Application.Services
{
    public class TOTPService : ITOTPService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITotpGenerator _totpGenerator;
        private readonly ITotpSetupGenerator _totpQrGenerator;
        private readonly ITotpValidator _totpValidator;
        private readonly UrlEncoder _urlEncoder;
        private const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";
        public string AuthenticatorIssuer { get; set; } = "Microsoft.AspNetCore.Identity.UI";

        public TOTPService(UserManager<AppUser> userManager, UrlEncoder urlEncoder)
        {
            _totpGenerator = new TotpGenerator();
            _totpValidator = new TotpValidator(_totpGenerator);
            _totpQrGenerator = new TotpSetupGenerator();
            _userManager = userManager;
            _urlEncoder = urlEncoder;
        }
        public async Task<Bitmap> GenerateQrCodeImage(string userName, string passWord)
        {
           
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                throw new GameShopException("Tài khoản hoặc mật khẩu không chính xác");
            }
            if (await _userManager.CheckPasswordAsync(user, passWord)== false)
            {
                throw new GameShopException("Tài khoản hoặc mật khẩu không chính xác");
            }

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(string.Format(
            AuthenticatorUriFormat,
                _urlEncoder.Encode(AuthenticatorIssuer),
                _urlEncoder.Encode(user.UserName),
                user.OTPValue), QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(5);
            return qrCodeImage;
        }
        public async Task<string> GenerateQrCodeUri(string userName, string passWord)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                throw new GameShopException("Tài khoản hoặc mật khẩu không chính xác");
            }
            if (await _userManager.CheckPasswordAsync(user, passWord) == false)
            {
                throw new GameShopException("Tài khoản hoặc mật khẩu không chính xác");
            }

            return string.Format(
            AuthenticatorUriFormat,
                _urlEncoder.Encode(AuthenticatorIssuer),
                _urlEncoder.Encode(user.UserName),
                user.OTPValue);
        }
        public async Task<string> GetCode(string userName, string passWord)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                throw new GameShopException("Tài khoản hoặc mật khẩu không chính xác");
            }
            if (await _userManager.CheckPasswordAsync(user, passWord) == false)
            {
                throw new GameShopException("Tài khoản hoặc mật khẩu không chính xác");
            }

            var secretKey = GenerateRecoveryCodes();
            user.OTPValue = secretKey;
            await _userManager.UpdateAsync(user);
            return secretKey;
        }

        //public async Task<TotpSetup> GetQR(string email)
        //{
        //    var user = await _userManager.FindByEmailAsync(email);
        //    if (user == null)
        //    {
        //        throw new GameShopException("Không tìm thấy tài khoản");
        //    }
        //    var qrCode = _totpQrGenerator.Generate(
        //        "GameShop",
        //        user.Id.ToString(),
        //       user.Id.ToString()
        //    );
        //    return qrCode;
        //}

        public async Task<bool> Validate(ValidateOTPDTO req)
        {
            var user = await _userManager.FindByNameAsync(req.userName);
            if (user == null)
            {
                throw new GameShopException("Tài khoản hoặc mật khẩu không chính xác");
            }
            if (await _userManager.CheckPasswordAsync(user, req.password) == false)
            {
                throw new GameShopException("Tài khoản hoặc mật khẩu không chính xác");
            }
            //var x = int.Parse(req.Code);
            //return _totpValidator.Validate(user.Id.ToString(), int.Parse(req.Code));
            var unixTimestamp = Convert.ToInt64(Math.Round((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds));
            var timestep = Convert.ToInt64(unixTimestamp / 30);

            var totp = new Totp(Base32Encoding.ToBytes(user.OTPValue));
            var isOTPValid = totp.VerifyTotp(req.Code, out timestep);
            if (isOTPValid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string GenerateRecoveryCodes()
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
            return new string(Enumerable.Repeat(chars, 32)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<bool> ForgotQRScan(string email)
        {
           if(email == null)
            {
                throw new GameShopException("Vui lòng nhập Email");
            }
           var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new GameShopException("Email không tồn tại");
            }
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(string.Format(
            AuthenticatorUriFormat,
                _urlEncoder.Encode(AuthenticatorIssuer),
                _urlEncoder.Encode(user.UserName),
                user.OTPValue), QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(5);
            MemoryStream ms = new MemoryStream();
            qrCodeImage.Save(ms, ImageFormat.Jpeg);
            byte[] byteImage = ms.ToArray();
            var SigBase64 = Convert.ToBase64String(byteImage);
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("stemgameshop@gmail.com");
                mail.To.Add(user.Email);
                mail.Subject = "Confirm Account";
                mail.Body = $@"<html>
                      <body>
                      <p>Dear {user.UserName},</p>
                     <td><img src=data:image/jpg;base64,{SigBase64}></td>
                      <p>Sincerely,<br>-STEM</br></p>
                      </body>
                      </html>
                     ";
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("stemgameshop@gmail.com", "tditidglubtzxojy");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
            return true;
        }
    }
}
