using FRT.MasterDataCore.Customs;
using GameShop.Application.Services.Carts;
using GameShop.Application.System.Users;
using GameShop.ViewModels.Catalog.Carts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Transactions;

namespace GameShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;
        readonly ITransactionCustom _transactionCustom;
        public CartsController(ICartService cartService, ITransactionCustom transactionCustom)
        {
            _cartService = cartService;
            _transactionCustom = transactionCustom;
        }

        [HttpPost("UserID")]
        public async Task<IActionResult> AddToCart(string UserID, [FromBody] CartCreateRequest cartCreateRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (var transaction = _transactionCustom.CreateTransaction(isolationLevel: IsolationLevel.ReadUncommitted))
            {
                var result = await _cartService.AddToCart(UserID, cartCreateRequest);
                if (!result.IsSuccess)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
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
            using (var transaction = _transactionCustom.CreateTransaction(isolationLevel: IsolationLevel.ReadUncommitted))
            {
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
}