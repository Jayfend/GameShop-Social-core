using GameShop.Application.Catalog.Games.Dtos.Manage;
using GameShop.Application.Catalog.Games.Dtos.Public;
using GameShop.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Application.Catalog.Games
{
    public interface IPublicGameService
    {
        public PagedResult<GameViewModel> GetAllbyGenreID(Dtos.Public.GetGamePagingRequest request);
    }
}
