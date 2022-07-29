using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Entities
{
    public class OrderedGame
    {
        public int OrderID { get; set; }
        public Game Game { get; set; }
        public Cart Cart { get; set; }
        public int CartID { get; set; }
        public int GameID { get; set; }
    }
}