using GameShop.ViewModels.Common;
using GameShop.ViewModels.System.Roles;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameShop.AdminApp.Services
{
    public interface IRoleApiClient
    {
        Task<ApiResult<List<RoleViewModel>>> GetAll();
    }
}