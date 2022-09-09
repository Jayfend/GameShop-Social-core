using GameShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Configurations
{
    public class OrderedGameConfiguration : IEntityTypeConfiguration<OrderedGame>
    {
        public void Configure(EntityTypeBuilder<OrderedGame> builder)
        {
            builder.ToTable("OrderedGames");
            builder.HasKey(x => x.OrderID);
            builder.HasOne(x => x.Cart).WithMany(x => x.OrderedGames).HasForeignKey(x => x.CartID);
            builder.HasOne(x => x.Game).WithMany(x => x.OrderedGames).HasForeignKey(x => x.GameID);
        }
    }
}