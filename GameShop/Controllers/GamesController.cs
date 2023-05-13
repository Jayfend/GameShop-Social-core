using GameShop.Application.Services.Games;
using GameShop.ViewModels.Catalog.GameImages;
using GameShop.ViewModels.Catalog.Games;
using GameShop.ViewModels.Catalog.UserImages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GameShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService, IWebHostEnvironment webHostEnvironment)
        {
            _gameService = gameService;
        }

        //https:://localhost:port/game
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetManageGamePagingRequest request)
        {
            var games = await _gameService.GetAll(request);
            return Ok(games);
        }

        /* https:://localhost:port/game/paging */

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetManageGamePagingRequest request)
        {
            var games = await _gameService.GetAllPaging(request);
            if (games == null)
            {
                return NotFound();
            }
            return Ok(games);
        }

        [HttpPost("active-game")]
        public async Task<IActionResult> ActiveGameAsync(ActiveGameDTO req)
        {
            var response = await _gameService.ActiveGameAsync(req);
           
            return Ok(response);
        }
        [HttpGet("bestseller")]
        public async Task<IActionResult> GetBestSeller([FromQuery] GetManageGamePagingRequest request)
        {
            var games = await _gameService.GetBestSeller(request);
            if (games == null)
            {
                return NotFound();
            }
            return Ok(games);
        }

        [HttpGet("getgamesale")]
        public async Task<IActionResult> GetGameSell([FromQuery] GetManageGamePagingRequest request)
        {
            var games = await _gameService.GetSaleGames(request);
            if (games == null)
            {
                return NotFound();
            }
            return Ok(games);
        }

        /* https:://localhost:port/game/1 */

        [HttpGet("{GameID}")]
        public async Task<IActionResult> GetById(Guid GameID)
        {
            var games = await _gameService.GetById(GameID);
            if (games == null)
            {
                return NotFound();
            }
            return Ok(games);
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetAll([FromQuery] GetManageGamePagingRequest request)
        {
            var games = await _gameService.GetAll(request);
            if (games == null)
            {
                return NotFound();
            }
            return Ok(games);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromForm] GameCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //var newgame = new GameCreateRequest()
            //{
            //    GameName = request.GameName,
            //    Price = request.Price,
            //    Discount = request.Discount,
            //    Description = request.Description,
            //    Gameplay = request.Gameplay,
            //    Genre = request.Genre,
            //    Status = request.Status,
            //    ThumbnailImage = request.ThumbnailImage,
            //    FileGame = request.FileGame,
            //    Publisher = request.Publisher,
            //    SRM = JsonConvert.DeserializeObject<SystemRequireMin>(request.SRM),
            //    SRR = JsonConvert.DeserializeObject<SystemRequirementRecommend>(request.SRR),
            //};
            var gameID = await _gameService.Create(request);
            if (gameID == Guid.Empty)
            {
                return BadRequest();
            }
            var game = await _gameService.GetById(gameID);
            return Created(nameof(GetById), game);
        }

        [HttpPut("{GameID}")]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update([FromRoute] Guid GameID, [FromForm] GameEditRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //var editgame = new GameEditRequest()
            //{
            //    GameID = GameID,
            //    Name = request.Name,
            //    Price = request.Price,
            //    Discount = request.Discount,
            //    Description = request.Description,
            //    Gameplay = request.Gameplay,
            //    Status = request.Status,
            //    ThumbnailImage = request.ThumbnailImage,
            //    Publisher = request.Publisher,
            //    SRM = JsonConvert.DeserializeObject<SystemRequireMin>(request.SRM),
            //    SRR = JsonConvert.DeserializeObject<SystemRequirementRecommend>(request.SRR),
            //};
            var affedtedResult = await _gameService.Update(GameID, request);
            if (affedtedResult == 0)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{GameID}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(Guid GameID)
        {
            var affedtedResult = await _gameService.Delete(GameID);
            if (affedtedResult == 0)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPatch("price/{GameID}/{newPrice}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdatePrice([FromRoute] Guid GameID, decimal newPrice)
        {
            var isSuccess = await _gameService.UpdatePrice(GameID, newPrice);
            if (isSuccess == false)
            {
                return BadRequest();
            }

            return Ok();
        }

        // Image
        [HttpPost("{GameID}/Images")]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateImage([FromRoute] Guid GameID, [FromForm] GameImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var imageid = await _gameService.AddImage(GameID, request);
                if (imageid == Guid.Empty)
                {
                    return BadRequest();
                }
                var image = await _gameService.GetImageById(imageid);
                return CreatedAtAction(nameof(GetImageByID), new { ImageID = imageid, GameID = GameID }, image);
            }
        }

        [HttpGet("{GameID}/Images/{ImageID}")]
        public async Task<IActionResult> GetImageByID(Guid GameID, Guid ImageID)
        {
            var image = await _gameService.GetImageById(ImageID);
            if (image == null)
            {
                return BadRequest("Could not find this image");
            }
            return Ok(image);
        }

        [HttpGet("{GameID}/images")]
        public async Task<IActionResult> GetListImages(Guid GameID)
        {
            var ListImage = await _gameService.GetListImages(GameID);
            if (ListImage == null)
            {
                return BadRequest("Could not find this image");
            }
            return Ok(ListImage);
        }

        [HttpPut("{GameID}/Images/{ImageID}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateImage(Guid ImageID, [FromForm] GameImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _gameService.UpdateImage(ImageID, request);
            if (result == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("{GameID}/Images/{ImageID}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteImage(Guid ImageID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _gameService.RemoveImage(ImageID);
            if (result == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut("{id}/genres")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CategoryAssign(Guid id, [FromBody] CategoryAssignRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _gameService.CategoryAssign(id, request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}