using GameShop.ViewModels.Catalog.Games;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.ViewModels.Catalog.Charts
{
    public class GameBuyCountModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int BuyCount { get; set; }
        public Decimal Total { get; set; }
    }
}