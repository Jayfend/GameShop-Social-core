using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.ViewModels.System.Users
{
    public class ValidateOTPDTO
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string Code { get; set; }
    }
}
