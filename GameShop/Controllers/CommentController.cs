using GameShop.Application.Services.Comments;
using GameShop.ViewModels.Catalog.Comments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GameShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [HttpPost]
        public async Task<IActionResult>CreateAsync([FromBody]CommentCreateReqDTO req)
        {
            var response = await _commentService.CreateComment(req);
            return Ok(response);
        }
    }
}
