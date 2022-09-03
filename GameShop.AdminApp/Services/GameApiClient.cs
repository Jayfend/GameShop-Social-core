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
using GameShop.Utilities.Constants;
using System.IO;
using System.Reflection;

namespace GameShop.AdminApp.Services
{
    public class GameApiClient :BaseApiClient,IGameApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public GameApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> CreateGame(GameCreateRequest request)
        {
            var sessions = _httpContextAccessor
                .HttpContext
                .Session
                .GetString(SystemConstants.AppSettings.Token);
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();

            if (request.ThumbnailImage != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.ThumbnailImage.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ThumbnailImage.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "thumbnailImage", request.ThumbnailImage.FileName);
            }

            requestContent.Add(new StringContent(request.Price.ToString()), "price");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.GameName) ? "" : request.GameName.ToString()), "gamename");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Description) ? "" : request.Description.ToString()), "description");

            requestContent.Add(new StringContent(request.Discount.ToString()), "discount");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Gameplay) ? "" : request.Gameplay.ToString()), "gameplay");
            requestContent.Add(new StringContent(request.Genre.ToString()), "genre");

            requestContent.Add(new StringContent(request.Status.ToString()), "status");

            var response = await client.PostAsync($"/api/games/", requestContent);
            return response.IsSuccessStatusCode;
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
