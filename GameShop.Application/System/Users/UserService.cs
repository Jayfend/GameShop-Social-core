using GameShop.Application.Common;
using GameShop.Application.Services;
using GameShop.Data.EF;
using GameShop.Data.Entities;
using GameShop.ViewModels.Catalog.UserImages;
using GameShop.ViewModels.Common;
using GameShop.ViewModels.System.Users;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Application.System.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly GameShopDbContext _context;
        private readonly IStorageService _storageService;
        private readonly ITOTPService _totpService;

        public UserService(UserManager<AppUser> useManager,
            SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IConfiguration config, GameShopDbContext context, IStorageService storageService,ITOTPService totpService)
        {
            _userManager = useManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
            _context = context;
            _storageService = storageService;
            _totpService = totpService;
        }

        public async Task<ApiResult<LoginResponse>> Authenticate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return new ApiErrorResult<LoginResponse>("Tài khoản không tồn tại");
            }
            var validateReq = new ValidateOTPDTO()
            {
                userName = request.UserName,
                password = request.Password,
                Code = request.Code,
            };
            if(await _totpService.Validate(validateReq)== false)
            {
                return new ApiErrorResult<LoginResponse>("Mã OTP hoặc mật khẩu sai");
            }

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<LoginResponse>("Đăng nhập không đúng");
            }
            else
            {
                if (user.isConfirmed == false)
                {
                    await _signInManager.SignOutAsync();
                    return new ApiErrorResult<LoginResponse>("Tài khoản chưa kích hoạt");
                }
            }
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {   new Claim("NameIdentifier",user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Role,String.Join(";",roles))
             };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            string _issuer = _config.GetValue<string>("Tokens:Issuer");
            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _issuer,
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            LoginResponse response = new LoginResponse()
            {
                UserId = user.Id.ToString(),
                isConfirmed = user.isConfirmed,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
            return new ApiSuccessResult<LoginResponse>(response);
        }

        public async Task<ApiResult<bool>> ChangePassword(PasswordUpdateRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return new ApiErrorResult<bool>("Tài khoản không tồn tại");
            }
            var hasher = new PasswordHasher<AppUser>();
            //var haspassword = hasher.HashPassword(null, request.Password);
            var check = hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (check.Equals((PasswordVerificationResult)0))
            {
                return new ApiErrorResult<bool>("Mật khẩu không đúng");
            }
            else
            {
                var newpassword = hasher.HashPassword(null, request.NewPassword);
                user.PasswordHash = newpassword;
                await _userManager.UpdateAsync(user);
                return new ApiSuccessResult<bool>();
            }
        }

        public async Task<ApiResult<bool>> Delete(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<bool>("User không tồn tại");
            }
            var reult = await _userManager.DeleteAsync(user);
            if (reult.Succeeded)
                return new ApiSuccessResult<bool>();

            return new ApiErrorResult<bool>("Xóa không thành công");
        }

        public async Task<ApiResult<bool>> ForgotPassword(ForgotPasswordRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return new ApiErrorResult<bool>("Tài khoản không tồn tại");
            }
            if (!user.Email.Equals(request.Email))
            {
                return new ApiErrorResult<bool>("Email không tồn tại");
            }
            if (!user.ConfirmCode.Equals(request.ConfirmCode))
            {
                return new ApiErrorResult<bool>("Mã xác nhận không đúng");
            }
            else
            {
                var hasher = new PasswordHasher<AppUser>();
                var newpassword = hasher.HashPassword(null, request.NewPassword);
                user.PasswordHash = newpassword;
                await _userManager.UpdateAsync(user);
                return new ApiSuccessResult<bool>();
            }
        }

        public async Task<ApiResult<UserViewModel>> GetById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<UserViewModel>("User không tồn tại");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var useravatar = await _context.UserAvatar.FirstOrDefaultAsync(x => x.UserID == id);
            var userthumbnail = await _context.UserThumbnail.FirstOrDefaultAsync(x => x.UserID == id);
            var userVm = new UserViewModel()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                Dob = user.Dob,
                Id = user.Id,
                LastName = user.LastName,
                UserName = user.UserName,
                Roles = roles,
                AvatarPath = useravatar.ImagePath,
                ThumbnailPath = userthumbnail.ImagePath
            };

            return new ApiSuccessResult<UserViewModel>(userVm);
        }

        public async Task<ApiResult<PagedResult<UserViewModel>>> GetUsersPaging(GetUserPagingRequest request)
        {
            var query = _userManager.Users;
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.UserName.Contains(request.Keyword) || x.PhoneNumber.Contains(request.Keyword));
            }
            int totalrow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                 .Take(request.PageSize)
                 .Select(x => new UserViewModel()
                 {
                     Email = x.Email,
                     PhoneNumber = x.PhoneNumber,
                     UserName = x.UserName,
                     FirstName = x.FirstName,
                     Id = x.Id,
                     LastName = x.LastName
                 }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<UserViewModel>()
            {
                TotalRecords = totalrow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<UserViewModel>>(pagedResult);
        }

        public async Task<ApiResult<bool>> Register(RegisterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                return new ApiErrorResult<bool>("Tài khoản đã tồn tại");
            }
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return new ApiErrorResult<bool>("Emai đã tồn tại");
            }
            var useravatar = new UserAvatar()
            {
                ImagePath = "imgnotfound.jpg",
            };
            var userthumbnail = new UserThumbnail()
            {
                ImagePath = "imgnotfound.jpg"
            };
            Random r = new Random();
            int randNum = r.Next(1000000);
            string sixDigitNumber = randNum.ToString("D6");
            user = new AppUser()
            {
                UserName = request.UserName,
                //Dob = request.Dob,
                Email = request.Email,
                UserAvatar = useravatar,
                UserThumbnail = userthumbnail,
                isConfirmed = false,
                ConfirmCode = sixDigitNumber,
                Creationtime = DateTime.Now
                //FirstName = request.FirstName,
                //LastName = request.LastName,
                //PhoneNumber = request.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _totpService.GetCode(user.UserName,request.Password);
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("stemgameshop@gmail.com");
                    mail.To.Add(user.Email);
                    mail.Subject = "Confirm Account";
                    mail.Body = $@"<html>
                      <body>
                      <p>Dear {user.UserName},</p>
                      <p>Thank for joining us,here is your confirm code {user.ConfirmCode}</p>
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
                return new ApiSuccessResult<bool>();
            }
            else
            {
                return new ApiErrorResult<bool>("Đăng ký không thành công");
            }
        }

        public async Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<bool>("Tài khoản không tồn tại");
            }
            var removedRoles = request.Roles.Where(x => x.Selected == false).Select(x => x.Name).ToList();
            foreach (var roleName in removedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == true)
                {
                    await _userManager.RemoveFromRoleAsync(user, roleName);
                }
            }
            await _userManager.RemoveFromRolesAsync(user, removedRoles);

            var addedRoles = request.Roles.Where(x => x.Selected).Select(x => x.Name).ToList();
            foreach (var roleName in addedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == false)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
            }

            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> UpdateUser(UserUpdateRequest request)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == request.Email && x.Id != request.Id))
            {
                return new ApiErrorResult<bool>("Emai đã tồn tại");
            }
            var user = await _userManager.FindByIdAsync(request.Id.ToString());

            user.Dob = request.Dob;
            user.Email = request.Email;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;
            user.LastUpdated = DateTime.Now;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            else
            {
                return new ApiErrorResult<bool>("Cập nhật không thành công");
            }
        }

        public async Task<ApiResult<string>> AddAvatar(string UserID, UserImageCreateRequest request)
        {
            if (request.ImageFile != null)
            {
                var getAvatar = await _context.UserAvatar.Where(x => x.UserID.ToString() == UserID).FirstOrDefaultAsync();
                if (getAvatar == null)
                {
                    var newAvatar = new UserAvatar()
                    {
                        UserID = new Guid(UserID),
                        UpdateDate = DateTime.Now,
                        ImagePath = await this.Savefile(request.ImageFile)
                    };
                    _context.UserAvatar.Add(newAvatar);
                    await _context.SaveChangesAsync();
                    return new ApiSuccessResult<string>(newAvatar.ImagePath);
                }
                else
                {
                    getAvatar.UpdateDate = DateTime.Now;
                    getAvatar.ImagePath = await this.Savefile(request.ImageFile);
                    _context.UserAvatar.Update(getAvatar);
                    await _context.SaveChangesAsync();
                    return new ApiSuccessResult<string>(getAvatar.ImagePath);
                }
            }
            else
            {
                return new ApiErrorResult<string>("Không tìm thấy hình ảnh");
            }
        }

        public async Task<ApiResult<string>> AddThumbnail(string UserID, UserImageCreateRequest request)
        {
            if (request.ImageFile != null)
            {
                var getThumbnail = await _context.UserThumbnail.Where(x => x.UserID.ToString() == UserID).FirstOrDefaultAsync();
                if (getThumbnail == null)
                {
                    var newThumbnail = new UserThumbnail()
                    {
                        UserID = new Guid(UserID),
                        UpdateDate = DateTime.Now,
                        ImagePath = await this.Savefile(request.ImageFile)
                    };
                    _context.UserThumbnail.Add(newThumbnail);
                    await _context.SaveChangesAsync();
                    return new ApiSuccessResult<string>(newThumbnail.ImagePath);
                }
                else
                {
                    getThumbnail.UpdateDate = DateTime.Now;
                    getThumbnail.ImagePath = await this.Savefile(request.ImageFile);
                    _context.UserThumbnail.Update(getThumbnail);
                    await _context.SaveChangesAsync();
                    return new ApiSuccessResult<string>(getThumbnail.ImagePath);
                }
            }
            else
            {
                return new ApiErrorResult<string>("Không tìm thấy hình ảnh");
            }
        }

        public async Task<string> Savefile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var filename = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), filename);
            return filename;
        }

        public async Task<ApiResult<bool>> ConfirmAccount(ConfirmAccountRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return new ApiErrorResult<bool>("Thông tin nhập không chính xác");
            }
            else
            {
                var hasher = new PasswordHasher<AppUser>();
                //var haspassword = hasher.HashPassword(null, request.Password);
                var check = hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
                if (check.Equals((PasswordVerificationResult)0))
                {
                    return new ApiErrorResult<bool>("Thông tin nhập không chính xác");
                }
                else
                {
                    if (user.ConfirmCode == request.ConfirmCode)
                    {
                        if (user.isConfirmed == false)
                        {
                            user.isConfirmed = true;

                            var result = await _userManager.UpdateAsync(user);
                            if (result.Succeeded)
                            {
                                return new ApiSuccessResult<bool>();
                            }
                            else
                            {
                                return new ApiErrorResult<bool>("Đã xảy ra lỗi");
                            }
                        }
                        else
                        {
                            return new ApiErrorResult<bool>("Tài khoản đã kích hoạt");
                        }
                    }
                    else
                    {
                        return new ApiErrorResult<bool>("Thông tin nhập không chính xác");
                    }
                }
            }
        }

        public async Task<ApiResult<bool>> SendEmail(SendEmailRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("stemgameshop@gmail.com");
                    mail.To.Add(user.Email);
                    mail.Subject = "Confirm Account";
                    mail.Body = $@"<html>
                      <body>
                      <p>Dear {user.UserName},</p>
                      <p>You are looking for the confirm code? here is your code {user.ConfirmCode}</p>
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
                return new ApiSuccessResult<bool>();
            }
            else
            {
                return new ApiErrorResult<bool>("Tài khoản không tồn tại");
            }
        }

        public async Task<ApiResult<bool>> AdminRegister(RegisterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                return new ApiErrorResult<bool>("Tài khoản đã tồn tại");
            }
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return new ApiErrorResult<bool>("Emai đã tồn tại");
            }
            var useravatar = new UserAvatar()
            {
                ImagePath = "imgnotfound.jpg",
            };
            var userthumbnail = new UserThumbnail()
            {
                ImagePath = "imgnotfound.jpg"
            };
            Random r = new Random();
            int randNum = r.Next(1000000);
            string sixDigitNumber = randNum.ToString("D6");
            user = new AppUser()
            {
                UserName = request.UserName,
                //Dob = request.Dob,
                Email = request.Email,
                UserAvatar = useravatar,
                UserThumbnail = userthumbnail,
                isConfirmed = true,
                ConfirmCode = sixDigitNumber,
                //FirstName = request.FirstName,
                //LastName = request.LastName,
                //PhoneNumber = request.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            else
            {
                return new ApiErrorResult<bool>("Đăng ký không thành công");
            }
        }

        public async Task<bool> DeleteInactiveAccount()
        {
            var userList = await _context.Users.ToListAsync();
            var deleteList = new List<AppUser>();
            foreach(var user in userList)
            {   var time = (DateTime.Now - user.Creationtime).TotalMinutes;
                var tenminute = new TimeSpan(0,10,0).TotalMinutes;
                if(user.isConfirmed == false &&  time > tenminute)
                {
                    deleteList.Add(user);
                }
            }
            _context.Users.RemoveRange(deleteList);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ApiResult<LoginResponse>> AdminAuthenticate(AdminLoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return new ApiErrorResult<LoginResponse>("Tài khoản không tồn tại");
            }

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<LoginResponse>("Đăng nhập không đúng");
            }
            else
            {
                if (user.isConfirmed == false)
                {
                    await _signInManager.SignOutAsync();
                    return new ApiErrorResult<LoginResponse>("Tài khoản chưa kích hoạt");
                }
            }
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {   new Claim("NameIdentifier",user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Role,String.Join(";",roles))
             };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            string _issuer = _config.GetValue<string>("Tokens:Issuer");
            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _issuer,
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            LoginResponse response = new LoginResponse()
            {
                UserId = user.Id.ToString(),
                isConfirmed = user.isConfirmed,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
            return new ApiSuccessResult<LoginResponse>(response);
        }
    }
}