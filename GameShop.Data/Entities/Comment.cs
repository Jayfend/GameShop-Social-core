using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Entities
{
    public class Comment : BaseEntity
    {
        public AppUser AppUser { get; set; }
        public Guid CreatorId { get; set; }
        public string Content { get; set; }
        public Post Post { get; set; }
        public Guid PostId { get; set; }


    }
}
