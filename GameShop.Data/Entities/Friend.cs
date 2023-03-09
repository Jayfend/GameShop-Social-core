using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Entities
{
    public class Friend : BaseEntity
    {
        public Guid UserId1 { get; set; }
        public Guid UserId2 { get; set; }
    }
}
