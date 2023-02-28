using GameShop.ViewModels.Catalog.GameImages;
using GameShop.ViewModels.Catalog.Games;
using GameShop.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameShop.AdminApp.Services
{
    public interface IGameApiClient
    {
        Task<PagedResult<GameViewModel>> GetGamePagings(GetManageGamePagingRequest request);

        Task<bool> CreateGame(GameCreateRequest request);

        Task<ApiResult<bool>> CategoryAssign(Guid id, CategoryAssignRequest request);

        Task<GameViewModel> GetById(Guid id);

        Task<bool> DeleteGame(int id);

        Task<bool> UpdateGame(GameEditRequest request);

        Task<bool> AddImage(int GameID, GameImageCreateRequest request);
    }
}