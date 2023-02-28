using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Entities
{
    public class Checkout : BaseEntity
    {
        public Cart Cart { get; set; }
        public Guid CartID { get; set; }
        public Decimal TotalPrice { get; set; }
        public DateTime Purchasedate { get; set; }
        public string Username { get; set; }
        public List<SoldGame> SoldGames { get; set; }
    }
}