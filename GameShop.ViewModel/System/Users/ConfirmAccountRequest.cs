using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.ViewModels.System.Users
{
    public class ConfirmAccountRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public string ConfirmCode { get; set; }
    }
}