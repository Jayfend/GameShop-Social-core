
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Entities
{
    public class Game : BaseEntity
    {
        public string GameName { get; set; }
        public Decimal Price { get; set; }
        public int Discount { get; set; }
        public string Description { get; set; }
        public string Gameplay { get; set; }
        public string Publisher { get; set; }
        public List<Comment> Comments { get; set; }
        public List<GameinGenre> GameInGenres { get; set; }
        public SystemRequirementMin SystemRequirementMin { get; set; }
        public SystemRequirementRecommended SystemRequirementRecommended { get; set; }
        public List<GameImage> GameImages { get; set; }
        public List<OrderedGame> OrderedGames { get; set; }
        public List<WishesGame> WishesGames { get; set; }
        public string FilePath { get; set; }
        public float RatePoint { get; set; }
        public List<Rating> Ratings { get; set; }
    }
}