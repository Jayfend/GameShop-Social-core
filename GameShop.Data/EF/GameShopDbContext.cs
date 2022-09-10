using GameShop.Data.Configurations;
using GameShop.Data.Entities;
using GameShop.Data.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace GameShop.Data.EF
{
    public class GameShopDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public GameShopDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GameConfiguration());
            modelBuilder.ApplyConfiguration(new GameinGenreConfguration());
            modelBuilder.ApplyConfiguration(new SRMConfiguration());
            modelBuilder.ApplyConfiguration(new SRRConfiguration());
            modelBuilder.ApplyConfiguration(new GameImageConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
            modelBuilder.ApplyConfiguration(new OrderedGameConfiguration());
            modelBuilder.ApplyConfiguration(new WishlistConfiguration());
            modelBuilder.ApplyConfiguration(new WishesGameConfiguration());
            modelBuilder.ApplyConfiguration(new CheckoutConfiguration());
            modelBuilder.ApplyConfiguration(new ContactConfiguration());
            modelBuilder.ApplyConfiguration(new UserAvatarConfiguration());
            modelBuilder.ApplyConfiguration(new UserThumbnailConfiguration());
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);

            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Seed();
        }

        public DbSet<GameinGenre> GameinGenres { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<SystemRequirementMin> SystemRequirementMin { get; set; }
        public DbSet<SystemRequirementRecommended> SystemRequirementRecommended { get; set; }
        public DbSet<GameImage> GameImages { get; set; }
        public DbSet<OrderedGame> OrderedGames { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<WishesGame> WishesGames { get; set; }
        public DbSet<Checkout> Checkouts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<UserAvatar> UserAvatar { get; set; }
        public DbSet<UserThumbnail> UserThumbnail { get; set; }
    }
}