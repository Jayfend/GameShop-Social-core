using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.ViewModels.System.Users
{
    public class PasswordUpdateRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
}