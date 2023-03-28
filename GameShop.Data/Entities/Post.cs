using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Entities
{
    public class Post : BaseEntity
    {
        public string Content { get; set; }
        public string ImagePath {get; set; }
        public List<Comment> Comments { get; set; } 
        public List<Like> Likes { get; set; }
        public AppUser AppUser { get; set; }
        public Guid CreatorId { get; set; }
        public string UserName { get; set; }
        
    }
}
