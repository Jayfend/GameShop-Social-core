using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Entities
{
    public class Genre : BaseEntity
    {
        public string GenreName { get; set; }
        public List<GameinGenre> GameInGenres { get; set; }
    }
}
