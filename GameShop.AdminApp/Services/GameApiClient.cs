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
using System.Text;
using GameShop.ViewModels.Catalog.GameImages;

namespace GameShop.AdminApp.Services
{
    public class GameApiClient : BaseApiClient, IGameApiClient
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

        public async Task<ApiResult<bool>> CategoryAssign(Guid id, CategoryAssignRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/games/{id}/genres", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
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
            if (request.FileGame != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.FileGame.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.FileGame.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "fileGame", request.FileGame.FileName);
            }
            requestContent.Add(new StringContent(request.Price.ToString()), "price");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.GameName) ? "" : request.GameName.ToString()), "gamename");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.PublisherId.ToString()) ? "" : request.PublisherId.ToString()), "publisher");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Description) ? "" : request.Description.ToString()), "description");

            requestContent.Add(new StringContent(request.Discount.ToString()), "discount");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Gameplay) ? "" : request.Gameplay.ToString()), "gameplay");
            requestContent.Add(new StringContent(request.Genre.ToString()), "genre");

            if (request.SRM != null)
            {
                var myContent = JsonConvert.SerializeObject(request.SRM);
                var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");
                requestContent.Add(stringContent, "srm");
            }
            else
            {
                var srmContent = new StringContent("");
                requestContent.Add(srmContent, "srm");
            }

            if (request.SRR != null)
            {
                var myContent = JsonConvert.SerializeObject(request.SRR);
                var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");
                requestContent.Add(stringContent, "srr");
            }
            else
            {
                var srrContent = new StringContent("");
                requestContent.Add(srrContent, "srr");
            }
            requestContent.Add(new StringContent(request.Status.ToString()), "status");

            var response = await client.PostAsync($"/api/games/", requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AddImage(int GameID, GameImageCreateRequest request)
        {
            var sessions = _httpContextAccessor
                .HttpContext
                .Session
                .GetString(SystemConstants.AppSettings.Token);
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var requestContent = new MultipartFormDataContent();

            if (request.ImageFile != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.ImageFile.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ImageFile.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "imagefile", request.ImageFile.FileName);
            }
            var myContent = JsonConvert.SerializeObject(request.isDefault);
            var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");
            requestContent.Add(stringContent, "isdefault");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Caption) ? "" : request.Caption.ToString()), "caption");

            requestContent.Add(new StringContent(request.SortOrder.ToString()), "sortorder");
            requestContent.Add(new StringContent(request.GameID.ToString()), "gameid");
            var response = await client.PostAsync($"/api/games/{GameID}/images", requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<GameViewModel> GetById(Guid id)
        {
            var data = await GetAsync<GameViewModel>($"/api/games/{id}");

            return data;
        }

        public async Task<PagedResult<GameViewModel>> GetGamePagings(GetManageGamePagingRequest request)
        {
            var data = await GetAsync<PagedResult<GameViewModel>>(
              $"/api/games/paging?pageIndex={request.PageIndex}" +
              $"&pageSize={request.PageSize}" +
              $"&keyword={request.Keyword}&GenreID={request.GenreID}");

            return data;
        }

        public async Task<bool> DeleteGame(int id)
        {
            return await Delete($"/api/games/" + id);
        }

        public async Task<bool> UpdateGame(GameEditRequest request)
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
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Name) ? "" : request.Name.ToString()), "name");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Description) ? "" : request.Description.ToString()), "description");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.PublisherId.ToString()) ? "" : request.PublisherId.ToString()), "publisher");
            requestContent.Add(new StringContent(request.Discount.ToString()), "discount");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Gameplay) ? "" : request.Gameplay.ToString()), "gameplay");

            if (request.SRM != null)
            {
                var myContent = JsonConvert.SerializeObject(request.SRM);
                var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");
                requestContent.Add(stringContent, "srm");
            }
            else
            {
                var srmContent = new StringContent("");
                requestContent.Add(srmContent, "srm");
            }

            if (request.SRR != null)
            {
                var myContent = JsonConvert.SerializeObject(request.SRR);
                var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");
                requestContent.Add(stringContent, "srr");
            }
            else
            {
                var srrContent = new StringContent("");
                requestContent.Add(srrContent, "srr");
            }
            requestContent.Add(new StringContent(request.Status.ToString()), "status");

            var response = await client.PutAsync($"/api/games/" + request.Id, requestContent);
            return response.IsSuccessStatusCode;
        }
    }
}