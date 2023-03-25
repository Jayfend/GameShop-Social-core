using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Entities
{
    public class SoldGame : BaseEntity
    {
        public Guid GameID { get; set; }
        public string GameName { get; set; }
        public Decimal Price { get; set; }
        public int Discount { get; set; }
        public Checkout Checkout { get; set; }
        public Guid CheckoutID { get; set; }
        public string ImagePath { get; set; }
        public string GameFile { get; set; }
    }
}