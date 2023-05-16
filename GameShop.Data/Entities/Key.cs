using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Entities
{
    public class Key : BaseEntity
    {
        public string KeyCode { get; set; }
        public string PublisherName { get; set; }
        public string GameName { get; set; }
        public bool IsActive { get; set; }
        public Publisher Publisher { get; set; }
        public Guid PublisherId { get; set; }
    }
}
