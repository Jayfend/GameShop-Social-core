using GameShop.ViewModels.Catalog.Comments;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Application.Services.Comments
{
    public interface ICommentService
    {
        public Task<CommentDTO> CreateComment(CommentCreateReqDTO req);
    }
}
