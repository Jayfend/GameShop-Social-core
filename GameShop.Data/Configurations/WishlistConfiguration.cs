using GameShop.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Configurations
{
    public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {
            builder.ToTable("Wishlists");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.AppUser)
                .WithOne(x => x.Wishlist)
                .HasForeignKey<Wishlist>(x => x.UserID);
          
        }
    }
}
