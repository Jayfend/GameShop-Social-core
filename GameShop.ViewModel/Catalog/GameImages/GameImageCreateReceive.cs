using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.ViewModels.Catalog.GameImages
{
    public class GameImageCreateReceive
    {
        public int GameID { get; set; }
        public string Caption { get; set; }
        public bool isDefault { get; set; }
        public int SortOrder { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}