using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Entities
{
    public class UserAvatar : BaseEntity
    {
        public AppUser AppUser { get; set; }
        public Guid UserID { get; set; }
        public DateTime UpdateDate { get; set; }
        public string ImagePath { get; set; }
    }
}