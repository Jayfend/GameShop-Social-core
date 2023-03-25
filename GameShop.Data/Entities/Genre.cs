using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Entities
{
    public class Genre
    {
        public int GenreID { get; set; }
        public string GenreName { get; set; }
        public List<GameinGenre> GameInGenres { get; set; }
    }
}
