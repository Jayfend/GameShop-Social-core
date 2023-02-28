using GameShop.Application.Catalog.Checkouts;
using GameShop.ViewModels.Catalog.Carts;
using GameShop.ViewModels.Catalog.Games;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GameShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CheckoutsController : ControllerBase
    {
        private readonly ICheckoutService _checkoutService;

        public CheckoutsController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        [HttpPost("UserID")]
        public async Task<IActionResult> Checkout(Guid UserID)
        {
            var result = await _checkoutService.CheckoutGame(UserID);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("CheckoutID")]
        public async Task<IActionResult> GetBill(Guid CheckoutID)
        {
            var result = await _checkoutService.GetBill(CheckoutID);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpGet("AllBill")]
        public async Task<IActionResult> GetAllBill()
        {
            var result = await _checkoutService.GetAllBill();
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpGet("paging/{UserID}")]
        public async Task<IActionResult> GetAllPaging(Guid UserID, [FromQuery] GetManageGamePagingRequest request)
        {
            var games = await _checkoutService.GetPurchasedGames(UserID, request);
            if (games == null)
            {
                return NotFound();
            }
            return Ok(games);
        }
    }
}