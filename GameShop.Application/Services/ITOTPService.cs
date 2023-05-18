using AspNetCore.Totp.Interface.Models;
using GameShop.ViewModels.Catalog;
using GameShop.ViewModels.System.Users;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Application.Services
{
    public interface ITOTPService
    {
        Task<bool> TurnOnOTP(OTPSwitchDTO req);
        Task<bool> TurnOffOTP(OTPSwitchDTO req);
        Task<bool> CheckIsOn(OTPCheckDTO req);
        //Task<TotpSetup> GetQR(string email);
        Task<bool> Validate(ValidateOTPDTO req);
        Task<Bitmap> GenerateQrCodeImage(string userName, string passWord);
        Task<string> GenerateQrCodeUri(string userName, string passWord);
        Task<bool> ForgotQRScan(string email);

    }
}
