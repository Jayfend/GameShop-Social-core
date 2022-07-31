﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.ViewModels.Catalog.Games
{
    public class GameViewModel
    {
        public int GameID { get; set; }
        public string GameName { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public string Description { get; set; }
        public string Gameplay { get; set; }
        public List<int> GenreIDs { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        
    }
}