using GameShop.AdminApp.Services;
using GameShop.Utilities.Constants;
using GameShop.ViewModels.Catalog.Games;
using GameShop.ViewModels.Common;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> Index(string keyword, int? GenreId, int pageIndex = 1, int pageSize = 10)
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
        public async Task<IActionResult> Create([FromForm] GameCreateRequest request, int GenreId)
        {
            if (!ModelState.IsValid)
                return View(request);

            request.Genre = GenreId;
            var result = await _gameApiClient.CreateGame(request);
            if (result)
            {
                TempData["result"] = "Thêm mới sản phẩm thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm sản phẩm thất bại");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> CategoryAssign(int id)
        {
            var roleAssignRequest = await GetCategoryAssignRequest(id);
            return View(roleAssignRequest);
        }

        [HttpPost]
        public async Task<IActionResult> CategoryAssign(CategoryAssignRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _gameApiClient.CategoryAssign(request.Id, request);

            if (result.IsSuccess)
            {
                TempData["result"] = "Cập nhật danh mục thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            var roleAssignRequest = await GetCategoryAssignRequest(request.Id);

            return View(roleAssignRequest);
        }

        private async Task<CategoryAssignRequest> GetCategoryAssignRequest(int id)
        {
            var productObj = await _gameApiClient.GetById(id);
            var categories = await _categoryApiClient.GetAll();
            var categoryAssignRequest = new CategoryAssignRequest();
            foreach (var role in categories)
            {
                categoryAssignRequest.Categories.Add(new SelectItem()
                {
                    Id = role.Id.ToString(),
                    Name = role.Name,
                    Selected = productObj.GenreName.Contains(role.Name)
                });
            }
            return categoryAssignRequest;
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(new GameDeleteRequest()
            {
                Id = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(GameDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _gameApiClient.DeleteGame(request.Id);
            if (result)
            {
                TempData["result"] = "Xóa sản phẩm thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Xóa không thành công");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var game = await _gameApiClient.GetById(id);
            var editVm = new GameEditRequest()
            {
                GameID = game.GameID,
                Description = game.Description,
                Name = game.Name,
                Price = game.Price,
                Discount = game.Discount,
                Gameplay = game.Gameplay,
            };
            return View(editVm);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Edit([FromForm] GameEditRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _gameApiClient.UpdateGame(request);
            if (result)
            {
                TempData["result"] = "Cập nhật sản phẩm thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Cập nhật sản phẩm thất bại");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _gameApiClient.GetById(id);
            return View(result);
        }
    }
}