using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.ViewModels.Catalog.Carts
{
    public class OrderItemResponse
    {
        public int GameId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public DateTime AddedDate { get; set; }
        public List<string> ImageList { get; set; }
        public List<string> GenreName { get; set; }
        public List<int> GenreIds { get; set; }
    }
}