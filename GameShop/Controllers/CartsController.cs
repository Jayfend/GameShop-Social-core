using GameShop.Application.Catalog.Carts;
using GameShop.Application.System.Users;
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
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("UserID")]
        public async Task<IActionResult> AddToCart(string UserID, [FromBody] CartCreateRequest cartCreateRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _cartService.AddToCart(UserID, cartCreateRequest);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("UserID")]
        public async Task<IActionResult> GetCart(string UserID)
        {
            var result = await _cartService.GetCart(UserID);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpDelete("UserID")]
        public async Task<IActionResult> DeleteItem(string UserID, [FromBody] OrderItemDelete orderItemDelete)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _cartService.DeleteItem(UserID, orderItemDelete);
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