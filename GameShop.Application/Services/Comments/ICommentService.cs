using GameShop.ViewModels.Catalog.Comments;
using GameShop.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Application.Services.Comments
{
    public interface ICommentService
    {
        public Task<CommentDTO> CreateComment(CommentCreateReqDTO req);
        public Task<PagedResult<CommentDTO>> GetComment(GetCommentRequest req);
    }
}
