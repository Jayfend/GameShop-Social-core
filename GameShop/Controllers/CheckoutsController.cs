using GameShop.Application.Catalog.Checkouts;
using GameShop.ViewModels.Catalog.Carts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Checkout(string UserID)
        {
            var result = await _checkoutService.CheckoutGame(UserID);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("CheckoutID")]
        public async Task<IActionResult> GetBill(int CheckoutID)
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
    }
}