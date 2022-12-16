using GameShop.Application.Catalog.Charts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GameShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private readonly IChartService _chartService;

        public ChartsController(IChartService chartService)
        {
            _chartService = chartService;
        }

        [HttpGet("BestSellerPerMonth/{Year}/{Month}")]
        public async Task<IActionResult> GameStatisticalByMonthAndYear(int Year, int Month)
        {
            var result = await _chartService.GameStatisticalByMonthAndYear(Year, Month);
            return Ok(result);
        }
    }
}