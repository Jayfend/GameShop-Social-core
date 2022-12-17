﻿using GameShop.Application.Catalog.Charts;
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

        [HttpGet("BestSellerPerMonthSortbyBuy/{Year}/{Month}/{Take}")]
        public async Task<IActionResult> GameStatisticalByMonthAndYearSortbyBuy(int Year, int Month, int Take)
        {
            var result = await _chartService.GameStatisticalByMonthAndYear(Year, Month, Take);
            return Ok(result);
        }

        [HttpGet("BestSellerPerMonthSortbyTotal/{Year}/{Month}/{Take}")]
        public async Task<IActionResult> GameStatisticalByMonthAndYearSortbyToTal(int Year, int Month, int Take)
        {
            var result = await _chartService.GameStatisticalByMonthAndYearSortbyTotal(Year, Month, Take);
            return Ok(result);
        }
    }
}