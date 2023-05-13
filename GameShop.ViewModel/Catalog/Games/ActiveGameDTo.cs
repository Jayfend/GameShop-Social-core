using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.ViewModels.Catalog.Games
{
    public class ActiveGameDTO
    {
        public Guid UserId { get; set; }
        public Guid GameId { get; set; }
        public string Key { get; set; }
    }
}
