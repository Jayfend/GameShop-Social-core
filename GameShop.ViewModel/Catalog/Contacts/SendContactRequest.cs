﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.ViewModels.Catalog.Contacts
{
    public class SendContactRequest
    {
        public string Email { get; set; }
        public string Titile { get; set; }
        public string Content { get; set; }
    }
}