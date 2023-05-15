using FRT.MasterDataCore.Customs;
using GameShop.Application.Services.Comments;
using GameShop.ViewModels.Catalog.Comments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Transactions;

namespace GameShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        readonly ITransactionCustom _transactionCustom;
        public CommentController(ICommentService commentService, ITransactionCustom transactionCustom)
        {
            _commentService = commentService;
            _transactionCustom = transactionCustom; 
        }
        [HttpPost]
        public async Task<IActionResult>CreateAsync([FromBody]CommentCreateReqDTO req)
        {
            using (var transaction = _transactionCustom.CreateTransaction(isolationLevel: IsolationLevel.ReadUncommitted))
            {
                var response = await _commentService.CreateComment(req);
                return Ok(response);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] GetCommentRequest req)
        {
            var response = await _commentService.GetComment(req);
            return Ok(response);
        }
    }
}
