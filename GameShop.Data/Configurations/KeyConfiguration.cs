using GameShop.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Configurations
{
    public class KeyConfiguration : IEntityTypeConfiguration<Key>
    {
        public void Configure(EntityTypeBuilder<Key> builder)
        {
            builder.ToTable("Keys");
            builder.HasKey(x => x.Id);
            builder.HasOne(x=>x.Publisher).WithMany(x=>x.Keys).HasForeignKey(x=>x.PublisherId);
          
        }
    }
}
