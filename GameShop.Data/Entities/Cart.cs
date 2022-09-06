using GameShop.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Entities
{
    public class Cart : BaseEntity
    {
        public int CartID { get; set; }
        public List<OrderedGame> OrderedGames { get; set; }
        public AppUser AppUser { get; set; }
        public Guid UserID { get; set; }

    }
}