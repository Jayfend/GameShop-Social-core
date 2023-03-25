using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Entities
{
    public class Wishlist : BaseEntity
    {
        public List<WishesGame> WishesGame { get; set; }
        public AppUser AppUser { get; set; }
        public Guid UserID { get; set; }
    }
}
