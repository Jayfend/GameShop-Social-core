using GameShop.Application.Services.Wishlists;
using GameShop.ViewModels.Catalog.Wishlists;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GameShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        [HttpPost("UserID")]
        public async Task<IActionResult> AddWishlist(Guid UserID, AddWishlistRequest addWishlistRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _wishlistService.AddWishlist(UserID, addWishlistRequest);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("UserID")]
        public async Task<IActionResult> GetWishlist(Guid UserID)
        {
            var result = await _wishlistService.GetWishlist(UserID);
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
        public async Task<IActionResult> DeleteItem(Guid UserID, [FromBody] DeleteItemRequest deleteItemRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _wishlistService.DeleteItem(UserID, deleteItemRequest);
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