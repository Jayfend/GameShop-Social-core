using GameShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameShop.Data.Configurations
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.ToTable("Ratings");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.AppUser)
                .WithMany(x => x.Ratings)
                .HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Game)
          .WithMany(x => x.Ratings)
          .HasForeignKey(x => x.GameId);
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.UpdatedDate).IsRequired();
        }
    }
}
