using GameShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Configurations
{
    public class SRRConfiguration : IEntityTypeConfiguration<SystemRequirementRecommended>
    {
        public void Configure(EntityTypeBuilder<SystemRequirementRecommended> builder)
        {
            builder.HasKey(x => x.SRRID);
            builder.HasOne(sr => sr.Game)
                  .WithOne(x => x.SystemRequirementRecommended)
                  .HasForeignKey<SystemRequirementRecommended>(sr => sr.GameID);
        }
    }
}
