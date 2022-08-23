using GameShop.AdminApp.Services;
using GameShop.ViewModels.Catalog.Games;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameShop.AdminApp.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameApiClient _gameApiClient;
        public GameController(IGameApiClient gameApiClient)
        {
            _gameApiClient = gameApiClient;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string keyword ="Grand" , int? GenreId = 2, int pageIndex = 1, int pageSize = 10)
        {   
            var request = new GetManageGamePagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                GenreID=GenreId,
            };
            
            var games = await _gameApiClient.GetGamePagings(request);
            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(games);
        }
    }
}
