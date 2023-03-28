using GameShop.Application.Services.Posts;
using GameShop.ViewModels.Catalog.Likes;
using GameShop.ViewModels.Catalog.Posts;
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
    public class PostsController : ControllerBase
    { IPostService _postService { get; set; }
        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] PostCreateReqModel req)
        {
            var response = await _postService.CreateAsync(req);
            return Ok(response);

        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromForm] PostUpdateReqModel req)
        {
            var response = await _postService.UpdateAsync(req);
            return Ok(response);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var response = await _postService.DeleteAsync(id);
            return Ok(response);
        }
        [HttpGet("detail/{id}")]
        public async Task<IActionResult>GetByIdAsync(Guid id)
        {
            var response = await _postService.GetByIdAsync(id);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] GetPostPagingRequest req)
        {
            var response = await _postService.GetAsync(req);
            return Ok(response);
        }
        [HttpPost("Like")]
        public async Task<IActionResult> LikeAsync([FromBody] LikeReqModel req)
        {
            var response = await _postService.LikeAsync(req);
            return Ok(response);
        }
    }
}
