using GameShop.Application.Catalog.Games;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GameShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IPublicGameService _publicGameService;
        public GameController(IPublicGameService publicGameService)
        {   _publicGameService = publicGameService;
           
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _publicGameService.GetAll();
            return Ok(products);
        }
    }
}
