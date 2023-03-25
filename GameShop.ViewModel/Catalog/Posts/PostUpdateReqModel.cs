using GameShop.ViewModels.Catalog.Comments;
using GameShop.ViewModels.Catalog.Likes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace GameShop.ViewModels.Catalog.Posts
{
    public class PostUpdateReqModel

    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public IFormFile Image { get; set; }
    }
    public class PostUpdateResModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Status { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public List<CommentDTO> Comments { get; set; }
        public List<LikeDTO> Likes { get; set; }
        public Guid CreatorId { get; set; }
    }
}
