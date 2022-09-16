using GameShop.ViewModels.Catalog.Contacts;
using GameShop.ViewModels.Catalog.Games;
using GameShop.ViewModels.Common;
using GameShop.ViewModels.System.Roles;
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
    public class ContactApiClient : IContactApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ContactApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)

        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResult<List<ContactViewModel>>> GetContacts()
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration.GetValue<string>("BaseAddress"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.GetAsync($"api/contact");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<List<ContactViewModel>>>(body);
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<List<ContactViewModel>>>(body);
        }
    }
}