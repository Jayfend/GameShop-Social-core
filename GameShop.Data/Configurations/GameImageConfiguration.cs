using GameShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameShop.Data.Configurations
{
    public class GameImageConfiguration : IEntityTypeConfiguration<GameImage>
    {
        public void Configure(EntityTypeBuilder<GameImage> builder)
        {
            builder.ToTable("GameImages");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasOne(x => x.Game).WithMany(x => x.GameImages).HasForeignKey(x => x.GameID);
            builder.Property(x => x.ImagePath).IsRequired();
            builder.Property(x => x.Caption).HasMaxLength(200).IsRequired();
        }
    }
}