using GameShop.Application;
using GameShop.Application.Services.Publishers;
using GameShop.ViewModels.Catalog.Publishers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Transactions;

namespace GameShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _publisherService;
       
        public PublisherController(IPublisherService publisherService) 
        {
            _publisherService = publisherService;
            

        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateAsync(PublisherCreateDTO req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
                var response = await _publisherService.CreateAsync(req);
                return Ok(response);
            
            
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _publisherService.GetAllPublisher();
            return Ok(response);
        }
        [HttpPost("Generate-key")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GenerateAsync(Guid publisherId, int amount)
        {
            
                var response = await _publisherService.GenerateKeyAsync(publisherId, amount);
                return Ok(response);
            
        }
    }
}
