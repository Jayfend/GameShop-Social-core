using GameShop.AdminApp.Services;
using GameShop.ViewModels.Catalog.Games;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.AdminApp.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameApiClient _gameApiClient;
        private readonly ICategoryApiClient _categoryApiClient;
        public GameController(IGameApiClient gameApiClient, ICategoryApiClient categoryApiClient)
        {
            _gameApiClient = gameApiClient;
            _categoryApiClient = categoryApiClient;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string keyword , int? GenreId, int pageIndex = 1, int pageSize = 10)
        {
            var request = new GetManageGamePagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                GenreID = GenreId,
            };

            var games = await _gameApiClient.GetGamePagings(request);
            ViewBag.Keyword = keyword;

            var categories = await _categoryApiClient.GetAll();
            ViewBag.Categories = categories.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = GenreId.HasValue && GenreId.Value == x.Id
            });
           
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(games);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryApiClient.GetAll();

            ViewBag.Categories = categories.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
              
            });
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] GameCreateRequest request,int? GenreId)
        {
            if (!ModelState.IsValid)
                return View(request);
            //request.Genrerequests.Add(GenreId);
            var result = await _gameApiClient.CreateGame(request);
            if (result)
            {
                TempData["result"] = "Thêm mới sản phẩm thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm sản phẩm thất bại");
            return View(request);
        }
    }
    
}
