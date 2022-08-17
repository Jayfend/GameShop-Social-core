using GameShop.ViewModels.Catalog.Games;
using GameShop.ViewModels.Common;
using GameShop.ViewModels.System.Users;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GameShop.AdminApp.Services
{
    public class GameApiClient :BaseApiClient,IGameApiClient
    {
        public GameApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            
        }
        public async Task<PagedResult<GameViewModel>> GetGamePagings(GetManageGamePagingRequest request)
        {
            var data = await GetAsync<PagedResult<GameViewModel>>(
              $"/api/games/paging?pageIndex={request.PageIndex}" +
              $"&pageSize={request.PageSize}" +
              $"&keyword={request.Keyword}&GenreID={request.GenreID}");

            return data;
        }
    }
}
