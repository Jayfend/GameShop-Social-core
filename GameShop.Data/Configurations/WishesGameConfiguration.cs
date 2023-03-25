using GameShop.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Configurations
{
    public class WishesGameConfiguration : IEntityTypeConfiguration<WishesGame>
    {
        public void Configure(EntityTypeBuilder<WishesGame> builder)
        {
            builder.ToTable("WishesGames");
            builder.HasKey(x => x.ID);
            builder.HasOne(x => x.Wishlist).WithMany(x => x.WishesGame).HasForeignKey(x => x.WishID);
            builder.HasOne(x => x.Game).WithMany(x => x.WishesGames).HasForeignKey(x => x.GameID);
        }
    }
}