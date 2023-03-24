using GameShop.ViewModels.Catalog.Comments;
using GameShop.ViewModels.Catalog.Likes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace GameShop.ViewModels.Catalog.Posts
{
    public class PostCreateReqModel
    {
        public Guid UserId { get; set; }
        public string Content { get; set; }
        public IFormFile Image { get; set; }
    }
    public class PostCreateResModel
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
