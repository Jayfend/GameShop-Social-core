using GameShop.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Configurations
{
    public class UserThumbnailConfiguration : IEntityTypeConfiguration<UserThumbnail>
    {
        public void Configure(EntityTypeBuilder<UserThumbnail> builder)
        {
            builder.ToTable("UserThumbnail");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ImagePath).IsRequired();
            builder.HasOne(x => x.AppUser).WithOne(x => x.UserThumbnail).HasForeignKey<UserThumbnail>(x => x.UserID);
        }
    }
}