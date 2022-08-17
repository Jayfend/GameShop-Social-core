using GameShop.ViewModels.Catalog.Games;
using GameShop.ViewModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameShop.AdminApp.Services
{
    public interface IGameApiClient
    {
        Task<PagedResult<GameViewModel>> GetGamePagings(GetManageGamePagingRequest request);
    }
}
