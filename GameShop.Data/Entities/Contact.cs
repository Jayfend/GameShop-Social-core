using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Titile { get; set; }
        public string Content { get; set; }
        public DateTime ReceiveDate { get; set; }
    }
}