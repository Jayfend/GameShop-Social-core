using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GameShop.ViewModels.Catalog.Games
{
    public class GameCreateRequest
    {
        public string GameName { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public string Description { get; set; }
        public string Gameplay { get; set; }
        public Guid Genre { get; set; }
        public bool Status { get; set; }
        public Guid PublisherId { get; set; }
        public IFormFile ThumbnailImage { get; set; }
        public IFormFile FileGame { get; set; }
        public SystemRequireMin SRM { get; set; } = new SystemRequireMin();
        public SystemRequirementRecommend SRR { get; set; } = new SystemRequirementRecommend();
    }
}