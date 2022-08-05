using GameShop.ViewModels.Common;
using GameShop.ViewModels.System.Users;
using System.Threading.Tasks;

namespace GameShop.AdminApp.Services
{
    public interface IUserApiClient
    {
        Task<string> Authenticate(LoginRequest request);

        Task<PagedResult<UserViewModel>> GetUsersPaging(GetUserPagingRequest request);
    }
}