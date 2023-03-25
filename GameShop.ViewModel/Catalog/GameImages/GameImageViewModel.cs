using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.ViewModels.Catalog.GameImages
{
    public class GameImageViewModel
    {
        public Guid GameID { get; set; }
        public Guid ImageID { get; set; }
        public string FilePath { get; set; }
        public bool isDefault { get; set; }
        public DateTime CreatedDate { get; set; }
        public long FileSize { get; set; }
        public string Caption { get; set; }
        public int SortOrder { get; set; }
    }
}