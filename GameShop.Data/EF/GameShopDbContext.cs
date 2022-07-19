using GameShop.Data.Configurations;
using GameShop.Data.Entities;
using GameShop.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.EF
{
    public class GameShopDbContext : DbContext
    {
        public GameShopDbContext(DbContextOptions options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GameConfiguration());
            modelBuilder.ApplyConfiguration(new BaseEntityConfiguration());
            modelBuilder.ApplyConfiguration(new GameinGenreConfguration());
            modelBuilder.ApplyConfiguration(new SRMConfiguration());
            modelBuilder.ApplyConfiguration(new SRRConfiguration());
            modelBuilder.ApplyConfiguration(new GameImageConfiguration());
            base.OnModelCreating(modelBuilder);

            modelBuilder.Seed();
        }
        public DbSet<GameinGenre> GameinGenres { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet <SystemRequirementMin> SystemRequirementMin { get; set; }
        public DbSet<SystemRequirementRecommended> SystemRequirementRecommended { get; set; }
        public DbSet<GameImage> GameImages { get; set; }
    }
}
