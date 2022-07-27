using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.ViewModels.Catalog.Games
{
    public class GameCreateRequest
    {
       
        public string GameName { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public string Description { get; set; }
        public string Gameplay { get; set; }
        public List<GenreCreateRequest> Genrerequests { get; set; }
        public int Status { get; set; }
        public IFormFile ThumbnailImage { get; set; }
    }
}
