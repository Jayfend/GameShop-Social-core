
using GameShop.ViewModels.Catalog.Games;
using GameShop.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Application.Catalog.Games
{
    public interface IPublicGameService
    {
        public Task<PagedResult<GameViewModel>> GetAllbyGenreID(GetPublicGamePagingRequest request);
        public Task<List<GameViewModel>> GetAll();
    }
}
