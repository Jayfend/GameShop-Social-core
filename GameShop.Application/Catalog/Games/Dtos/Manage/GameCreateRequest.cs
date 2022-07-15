using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Application.Catalog.Games.Dtos.Manage
{
    public class GameCreateRequest
    {
        public string GameName { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public string Description { get; set; }
        public string Gameplay { get; set; }
    }
}
