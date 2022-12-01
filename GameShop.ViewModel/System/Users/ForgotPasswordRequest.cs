using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.ViewModels.System.Users
{
    public class ForgotPasswordRequest
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmCode { get; set; }
    }
}