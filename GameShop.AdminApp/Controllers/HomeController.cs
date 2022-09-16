using GameShop.AdminApp.Models;
using GameShop.AdminApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.AdminApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IContactApiClient _contactApiClient;

        public HomeController(ILogger<HomeController> logger, IContactApiClient contactApiClient)
        {
            _logger = logger;
            _contactApiClient = contactApiClient;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _contactApiClient.GetContacts();

            return View(result.ResultObj);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}