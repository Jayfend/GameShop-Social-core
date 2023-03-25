using GameShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Configurations
{
    public class GameinGenreConfguration : IEntityTypeConfiguration<GameinGenre>
    {
        public void Configure(EntityTypeBuilder<GameinGenre> builder)
        {       
            builder.ToTable("GameinGenre");
            builder.HasOne(t => t.Game).WithMany(gg=>gg.GameInGenres).HasForeignKey(gg=>gg.GameId);
            builder.HasOne(t => t.Genre).WithMany(gg => gg.GameInGenres).HasForeignKey(gg => gg.GenreID);
        }
    }
}
