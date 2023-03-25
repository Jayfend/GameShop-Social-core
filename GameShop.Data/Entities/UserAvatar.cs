using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Entities
{
    public class UserAvatar
    {
        public int ImageID { get; set; }
        public AppUser AppUser { get; set; }
        public Guid UserID { get; set; }
        public DateTime UpdateDate { get; set; }
        public string ImagePath { get; set; }
    }
}