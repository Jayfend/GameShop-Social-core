using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Entities
{
    public class Rating : BaseEntity
    {
        public Guid GameId { get; set; }
        public Game Game { get; set; }
        public int Point { get; set; }
        public Guid UserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
