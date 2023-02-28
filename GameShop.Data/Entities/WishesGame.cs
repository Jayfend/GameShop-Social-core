using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Entities
{
    public class WishesGame : BaseEntity
    {

        public Game Game { get; set; }
        public Wishlist Wishlist { get; set; }
        public Guid WishID { get; set; }
        public Guid GameID { get; set; }
    }
}