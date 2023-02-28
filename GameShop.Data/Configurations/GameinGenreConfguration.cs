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
            builder.HasKey(t => new { t.GenreID, t.Id });
            builder.ToTable("GameinGenre");
            builder.HasOne(t => t.Game).WithMany(gg=>gg.GameInGenres).HasForeignKey(gg=>gg.Id);
            builder.HasOne(t => t.Genre).WithMany(gg => gg.GameInGenres).HasForeignKey(gg => gg.GenreID);
        }
    }
}
