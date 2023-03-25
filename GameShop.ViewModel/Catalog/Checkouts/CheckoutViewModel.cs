using GameShop.ViewModels.Catalog.Games;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.ViewModels.Catalog.Checkouts
{
    public class CheckoutViewModel
    {
        public int CartID { get; set; }
        public Decimal TotalPrice { get; set; }
        public DateTime Purchasedate { get; set; }
        public string Username { get; set; }
        public List<GameViewModel> Listgame { get; set; }
    }
}