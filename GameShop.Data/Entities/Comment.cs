using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Entities
{
    public class Comment : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid GameId { get; set; }
        public string Content {  get; set; }
        public AppUser AppUser { get; set; }
        public Game Game { get; set; }

    }
}
