using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Entities
{
    public class OrderedGame : BaseEntity
    {
        public Game Game { get; set; }
        public Cart Cart { get; set; }
        public Guid CartID { get; set; }
        public Guid GameID { get; set; }
    }
}