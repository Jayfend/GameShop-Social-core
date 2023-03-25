using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Entities
{
    public class GameImage : BaseEntity
    {
        public Guid GameID { get; set; }
        public string ImagePath { get; set; }
        public string Caption { get; set; }
        public bool isDefault { get; set; }
        public int SortOrder { get; set; }
        public long Filesize { get; set; }
        public Game Game { get; set; }
    }
}