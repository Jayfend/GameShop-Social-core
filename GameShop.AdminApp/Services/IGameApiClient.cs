﻿using GameShop.ViewModels.Catalog.Games;
using GameShop.ViewModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameShop.AdminApp.Services
{
    public interface IGameApiClient
    {
        Task<PagedResult<GameViewModel>> GetGamePagings(GetManageGamePagingRequest request);
        Task<bool> CreateGame(GameCreateRequest request);
        Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request);
        Task<GameViewModel> GetById(int id);
    }
}
