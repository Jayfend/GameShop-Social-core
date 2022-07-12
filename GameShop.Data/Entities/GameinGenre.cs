using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Entities
{
  public class GameinGenre
    {
        public int GameID { get; set; }
        public Game Game { get; set; }
        public int GenreID { get; set; }
        public Genre Genre { get; set; }
    }
}
