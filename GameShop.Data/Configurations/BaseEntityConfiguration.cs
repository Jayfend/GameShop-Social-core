using GameShop.Data.Entities;
using GameShop.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Configurations
{
    public class BaseEntityConfiguration : IEntityTypeConfiguration<BaseEntity>
    {
        public void Configure(EntityTypeBuilder<BaseEntity> builder)
        {
            builder.ToTable("BaseEntity");
            builder.HasKey(x => x.BaseEntityID);
            builder.Property(x => x.Status).HasDefaultValue(Status.Active);
        }
    }
}
