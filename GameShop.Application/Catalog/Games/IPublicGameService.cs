﻿using GameShop.Application.Catalog.Games.Dtos.Manage;
using GameShop.Application.Catalog.Games.Dtos.Public;
using GameShop.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Application.Catalog.Games
{
    public interface IPublicGameService
    {
        public Task<PagedResult<GameViewModel>> GetAllbyGenreID(Dtos.Public.GetGamePagingRequest request);
    }
}
