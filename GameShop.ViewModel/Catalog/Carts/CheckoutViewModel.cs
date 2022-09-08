using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.ViewModels.Catalog.Carts
{
    public class CheckoutViewModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
    }
}