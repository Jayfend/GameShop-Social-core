using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Entities
{
    public class Wishlist
    {
        public int ID { get; set; }
        public List<WishesGame> WishesGame { get; set; }
        public AppUser AppUser { get; set; }
        public Guid UserID { get; set; }
    }
}
