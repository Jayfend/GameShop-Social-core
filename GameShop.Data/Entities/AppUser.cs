using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Dob { get; set; }

        public List<Cart> Carts { get; set; }
        public Wishlist Wishlist { get; set; }
        public UserAvatar UserAvatar { get; set; }
        public UserThumbnail UserThumbnail { get; set; }
    }
}