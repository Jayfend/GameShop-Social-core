using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.ViewModels.Catalog.Comments
{
    public class CommentCreateReqDTO
    {
        public Guid UserId { get; set; }
        public Guid GameId { get; set; }
        public string Content { get; set; }
        public int Point { get; set; }
       
    }
}
