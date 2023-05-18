using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.ViewModels.Catalog
{
    public class OTPSwitchDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class OTPCheckDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
