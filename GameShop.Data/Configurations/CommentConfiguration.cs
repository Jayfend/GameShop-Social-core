using GameShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Checkouts");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Cart)
               .WithOne(x => x.Checkout)
               .HasForeignKey<Checkout>(x => x.CartID);
            builder.Property(x => x.Purchasedate).IsRequired();
            builder.Property(x => x.TotalPrice).IsRequired();
            builder.Property(x => x.Username).IsRequired();
        }
    }
}
