using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.ViewModels.Catalog.GameImages
{
    public class GameImageViewModel
    {
        public int GameID { get; set; }
        public int ImageID { get; set; }
        public string FilePath { get; set; }
        public bool isDefault { get; set; }
        public DateTime CreatedDate { get; set; }
        public long FileSize { get; set; }
        public string Caption { get; set; }
        public int SortOrder { get; set; }
    }
}