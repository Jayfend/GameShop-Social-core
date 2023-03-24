using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Entities
{
  public class GameinGenre : BaseEntity
    {
        public Guid GameId { get; set; }
        public Game Game { get; set; }
        public Guid GenreID { get; set; }
        public Genre Genre { get; set; }
    }
}
