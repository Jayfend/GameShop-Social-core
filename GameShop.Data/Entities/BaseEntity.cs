using GameShop.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Entities
{
    public class BaseEntity
    {   public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Status Status { get; set; }
    }
}
