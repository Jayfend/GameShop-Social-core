using GameShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Configurations
{
    public class UserAvatarConfiguration : IEntityTypeConfiguration<UserAvatar>
    {
        public void Configure(EntityTypeBuilder<UserAvatar> builder)
        {
            builder.ToTable("UserAvatar");
            builder.HasKey(x => x.ImageID);
            builder.Property(x => x.ImagePath).IsRequired();
            builder.HasOne(x => x.AppUser).WithOne(x => x.UserAvatar).HasForeignKey<UserAvatar>(x => x.UserID);
        }
    }
}