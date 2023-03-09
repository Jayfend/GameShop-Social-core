using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Entities
{
    public class Post : BaseEntity
    {
        public Guid UserId  { get; set; }
        public string Content { get; set; }
        public List<Comment> Comments { get; set; } 
        public List<Like> Likes { get; set; }
        public AppUser AppUser { get; set; }
    }
}
