using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace GameShop.Data.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Dob { get; set; }

        public List<Cart> Carts { get; set; }
        public List<Comment> Comments { get; set; }
        public Wishlist Wishlist { get; set; }
        public UserAvatar UserAvatar { get; set; }
        public UserThumbnail UserThumbnail { get; set; }
        public bool isConfirmed { get; set; }
        public string ConfirmCode { get; set; }
        public string Room { get; set; }
        public List<Rating> Ratings { get; set; }
        public string OTPValue { get; set; }
    }
}