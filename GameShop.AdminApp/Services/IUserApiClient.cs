using GameShop.ViewModels.Common;
using GameShop.ViewModels.System.Users;
using System;
using System.Threading.Tasks;

namespace GameShop.AdminApp.Services
{
    public interface IUserApiClient
    {
        Task<ApiResult<LoginResponse>> Authenticate(LoginRequest request);

        Task<ApiResult<PagedResult<UserViewModel>>> GetUsersPaging(GetUserPagingRequest request);

        Task<ApiResult<bool>> RegisterUser(RegisterRequest request);

        Task<ApiResult<bool>> UpdateUser(UserUpdateRequest request);

        Task<ApiResult<UserViewModel>> GetById(Guid id);

        Task<ApiResult<bool>> Delete(Guid id);

        Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);
    }
}