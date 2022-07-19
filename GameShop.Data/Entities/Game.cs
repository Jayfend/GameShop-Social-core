using GameShop.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Entities
{
    public class Game : BaseEntity
    { 
        public int GameID { get; set; }
        public string GameName { get; set; }
        public Decimal Price { get; set; }
        public int Discount { get; set; }
        public string Description { get; set; }
        public string Gameplay { get; set; }
        public List<GameinGenre> GameInGenres { get; set; }
        public SystemRequirementMin SystemRequirementMin { get; set; }
        public SystemRequirementRecommended SystemRequirementRecommended { get; set; }
        public List<GameImage> GameImages { get; set; }
    }
}
