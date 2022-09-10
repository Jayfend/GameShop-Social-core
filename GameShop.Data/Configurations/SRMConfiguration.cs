using GameShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Configurations
{
    public class SRMConfiguration : IEntityTypeConfiguration<SystemRequirementMin>
    {
        public void Configure(EntityTypeBuilder<SystemRequirementMin> builder)
        {
            builder.HasKey(x => x.SRMID);
            builder.HasOne(sr => sr.Game)
                   .WithOne(x => x.SystemRequirementMin)
                   .HasForeignKey<SystemRequirementMin>(sr => sr.GameID);
        }
    }
}