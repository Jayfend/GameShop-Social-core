using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.ViewModels.Catalog.Games
{
    public class GameEditRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public string Gameplay { get; set; }
        public int Status { get; set; }
        public IFormFile ThumbnailImage { get; set; }
        public SystemRequireMin SRM { get; set; } = new SystemRequireMin();
        public SystemRequirementRecommend SRR { get; set; } = new SystemRequirementRecommend();
        public IFormFile FileGame { get; set; }
    }
}