using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Entities
{
    public class Publisher : BaseEntity
    {
        public string Name { get; set; }
        public List<Game> Games { get; set; }
    }
}
