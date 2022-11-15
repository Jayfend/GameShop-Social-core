using GameShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Configurations
{
    public class SoldGameConfiguration : IEntityTypeConfiguration<SoldGame>
    {
        public void Configure(EntityTypeBuilder<SoldGame> builder)
        {
            builder.ToTable("SoldGames");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Checkout).WithMany(x => x.SoldGames).HasForeignKey(x => x.CheckoutID);
        }
    }
}