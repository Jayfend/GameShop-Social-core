using GameShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Configurations
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable("Games");
            builder.HasKey(x => x.GameID);
            builder.Property(x => x.GameName).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Discount).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Gameplay).IsRequired();
        }
    }
}