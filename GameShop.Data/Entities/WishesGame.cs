using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Entities
{
    public class WishesGame
    {
        public int ID { get; set; }
        public Game Game { get; set; }
        public Wishlist Wishlist { get; set; }
        public int WishID { get; set; }
        public int GameID { get; set; }
        public DateTime AddedDate { get; set; }
    }
}