using GameShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Configurations
{
    internal class SRMConfiguration : IEntityTypeConfiguration<SystemRequirementMin>
    {
        public void Configure(EntityTypeBuilder<SystemRequirementMin> builder)
        {
            builder.HasKey(x => x.SRMID);
        }
    }
}
