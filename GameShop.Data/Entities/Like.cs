using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Entities
{
    public class Like : BaseEntity
    {
        public Guid UserId { get; set; }
        public bool IsLiked { get; set; }
        public AppUser AppUser { get; set; }
    }
}
