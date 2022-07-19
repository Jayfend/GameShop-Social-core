using GameShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GameShop.Data.Configurations
{
    public class GameImageConfiguration : IEntityTypeConfiguration<GameImage>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<GameImage> builder)
        {
            builder.ToTable("GameImages");
            builder.HasKey(x => x.ImageID);
            builder.Property(x => x.ImageID).UseIdentityColumn();
            builder.HasOne(x => x.Game).WithMany(x => x.GameImages).HasForeignKey(x => x.GameID);
            builder.Property(x => x.ImagePath).IsRequired();
            builder.Property(x => x.Caption).HasMaxLength(200).IsRequired();
           
        }
    }
}   
