using GameShop.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Configurations
{
    public class CheckoutConfiguration : IEntityTypeConfiguration<Checkout>
    {
        public void Configure(EntityTypeBuilder<Checkout> builder)
        {
            builder.ToTable("Checkouts");
            builder.HasKey(x => x.ID);
            builder.HasOne(x => x.Cart)
               .WithOne(x => x.Checkout)
               .HasForeignKey<Checkout>(x => x.CartID);
            builder.Property(x => x.Purchasedate).IsRequired();
            builder.Property(x => x.TotalPrice).IsRequired();
            builder.Property(x => x.Username).IsRequired();
        }
    }
}