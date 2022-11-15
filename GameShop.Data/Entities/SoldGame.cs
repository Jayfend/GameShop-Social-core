﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Entities
{
    public class SoldGame
    {
        public int Id { get; set; }
        public int GameID { get; set; }
        public string GameName { get; set; }
        public Decimal Price { get; set; }
        public int Discount { get; set; }
        public Checkout Checkout { get; set; }
        public int CheckoutID { get; set; }
        public List<GameImage> GameImages { get; set; }
    }
}