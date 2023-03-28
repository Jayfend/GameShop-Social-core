using GameShop.ViewModels.Catalog.Comments;
using GameShop.ViewModels.Catalog.Likes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.ViewModels.Catalog.Posts
{
    public class PostResModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Status { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public bool IsLike { get; set; }
        public Guid CreatorId { get; set; }
    }
}
