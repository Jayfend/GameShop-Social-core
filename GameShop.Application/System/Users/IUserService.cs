using GameShop.Data.Entities;
using GameShop.ViewModels.Catalog.UserImages;
using GameShop.ViewModels.Common;
using GameShop.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Application.System.Users
{
    public interface IUserService
    {
        Task<ApiResult<LoginResponse>> Authenticate(LoginRequest request);

        Task<ApiResult<bool>> Register(RegisterRequest request);

        Task<ApiResult<bool>> AdminRegister(RegisterRequest request);

        Task<ApiResult<bool>> ConfirmAccount(ConfirmAccountRequest request);

        Task<ApiResult<bool>> UpdateUser(UserUpdateRequest request);

        Task<ApiResult<PagedResult<UserViewModel>>> GetUsersPaging(GetUserPagingRequest request);

        Task<ApiResult<UserViewModel>> GetById(Guid id);

        Task<ApiResult<bool>> Delete(Guid id);

        Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);

        Task<ApiResult<bool>> ChangePassword(PasswordUpdateRequest request);

        Task<ApiResult<bool>> ForgotPassword(ForgotPasswordRequest request);

        Task<ApiResult<string>> AddAvatar(string UserID, UserImageCreateRequest request);

        Task<ApiResult<string>> AddThumbnail(string UserID, UserImageCreateRequest request);

        Task<ApiResult<bool>> SendEmail(SendEmailRequest request);
    }
}