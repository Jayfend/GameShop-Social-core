using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.ViewModels.Catalog.Games
{
    public class GameCreateReceive
    {
        public string GameName { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public string Description { get; set; }
        public string Gameplay { get; set; }
        public int Genre { get; set; }
        public int Status { get; set; }
        public IFormFile ThumbnailImage { get; set; }
        public string Publisher { get; set; }
        public string SRM { get; set; }
        public string SRR { get; set; }
    }
}