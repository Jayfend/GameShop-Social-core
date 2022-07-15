using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Application.Catalog.Games.Dtos.Manage
{
    public class GameViewModel 
    {
        public int GameID { get; set; }
        public string GameName { get; set; }
        public Decimal Price { get; set; }
        public int Discount { get; set; }
        public string Description { get; set; }
        public string Gameplay { get; set; }
        public int GenreID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
