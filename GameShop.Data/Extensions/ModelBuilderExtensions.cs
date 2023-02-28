using GameShop.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            //Data seeding
            modelBuilder.Entity<Genre>().HasData(
                new Genre()
                {
                    Id = Guid.NewGuid(),
                    GenreName = "Action"
                });
            modelBuilder.Entity<Genre>().HasData(
               new Genre()
               {
                   Id = Guid.NewGuid(),
                   GenreName = "Open-World"
               });
            modelBuilder.Entity<Genre>().HasData(
               new Genre()
               {
                   Id = Guid.NewGuid(),
                   GenreName = "Multiplayer"
               });
            modelBuilder.Entity<Genre>().HasData(
               new Genre()
               {
                   Id = Guid.NewGuid(),
                   GenreName = "Action RPG"
               });
            modelBuilder.Entity<Genre>().HasData(
               new Genre()
               {
                   Id = Guid.NewGuid(),
                   GenreName = "Simulation"
               });
            modelBuilder.Entity<Genre>().HasData(
               new Genre()
               {
                   Id = Guid.NewGuid(),
                   GenreName = "Horror"
               });
            modelBuilder.Entity<Genre>().HasData(
               new Genre()
               {
                   Id = Guid.NewGuid(),
                   GenreName = "Sports & Racing"
               });
            modelBuilder.Entity<Genre>().HasData(
               new Genre()
               {
                   Id = Guid.NewGuid(),
                   GenreName = "Role-Playing"
               });
            modelBuilder.Entity<Genre>().HasData(
               new Genre()
               {
                   Id = Guid.NewGuid(),
                   GenreName = "Visual Novel"
               });

            modelBuilder.Entity<Game>().HasData(
                new Game()
                {
                    Id = Guid.NewGuid(),
                    GameName = "Grand Theft Auto V",
                    Price = 250000,
                    Discount = 0,
                    Description = "The best game in the world",
                    Gameplay = "Destroy the city",
                    CreatedDate = DateTime.Now,
                    Status = Enums.Status.Active,
                    UpdatedDate = DateTime.Now,
                });
            modelBuilder.Entity<Game>().HasData(
               new Game()
               {
                   Id = Guid.NewGuid(),
                   GameName = "Red Dead Redemption 2",
                   Price = 250000,
                   Discount = 20,
                   Description = "Back to the cowboy town",
                   Gameplay = "Discover the cowboy world",
                   CreatedDate = DateTime.Now,
                   Status = Enums.Status.Active,
                   UpdatedDate = DateTime.Now,
               });
            modelBuilder.Entity<GameinGenre>().HasData(
                new GameinGenre() { GenreID = Guid.NewGuid(), Id = Guid.NewGuid() },
                new GameinGenre() { GenreID = Guid.NewGuid(), Id = Guid.NewGuid() }
            );
            modelBuilder.Entity<GameinGenre>().HasData(
                new GameinGenre() { GenreID = Guid.NewGuid(), Id = Guid.NewGuid() },
                new GameinGenre() { GenreID = Guid.NewGuid(), Id = Guid.NewGuid() }
            );
            modelBuilder.Entity<SystemRequirementMin>().HasData(
                new SystemRequirementMin()
                {
                    Id = Guid.NewGuid(),
                    OS = "Windows 10 64 Bit, Windows 8.1 64 Bit, Windows 8 64 Bit, Windows 7 64 Bit Service Pack 1",
                    Processor = "Intel Core 2 Quad CPU Q6600 @ 2.40GHz (4 CPUs) / AMD Phenom 9850 Quad-Core Processor (4 CPUs) @ 2.5GHz",
                    Memory = "4 GB RAM",
                    Graphics = "NVIDIA 9800 GT 1GB / AMD HD 4870 1GB (DX 10, 10.1, 11)",
                    Storage = "72 GB available space",
                    AdditionalNotes = "",
                    GameID = 1,
                    Soundcard = "100% DirectX 10 compatible"
                });
            modelBuilder.Entity<SystemRequirementMin>().HasData(
              new SystemRequirementMin()
              {
                  Id = Guid.NewGuid(),
                  OS = "Windows 10 64 Bit, Windows 8.1 64 Bit, Windows 8 64 Bit, Windows 7 64 Bit Service Pack 1",
                  Processor = "Intel Core 2 Quad CPU Q6600 @ 2.40GHz (4 CPUs) / AMD Phenom 9850 Quad-Core Processor (4 CPUs) @ 2.5GHz",
                  Memory = "4 GB RAM",
                  Graphics = "NVIDIA 9800 GT 1GB / AMD HD 4870 1GB (DX 10, 10.1, 11)",
                  Storage = "72 GB available space",
                  AdditionalNotes = "",
                  GameID = 2,
                  Soundcard = "100% DirectX 10 compatible"
              });
            modelBuilder.Entity<SystemRequirementRecommended>().HasData(
                new SystemRequirementRecommended()
                {
                    Id = Guid.NewGuid(),
                    OS = "Windows 10 64 Bit, Windows 8.1 64 Bit, Windows 8 64 Bit, Windows 7 64 Bit Service Pack 1",
                    Processor = " Intel Core i5 3470 @ 3.2GHz (4 CPUs) / AMD X8 FX-8350 @ 4GHz (8 CPUs)",
                    Memory = "8 GB RAM",
                    Graphics = "NVIDIA GTX 660 2GB / AMD HD 7870 2GB",
                    Storage = "72 GB available space",
                    AdditionalNotes = "",
                    GameID = 1,
                    Soundcard = "100% DirectX 10 compatible"
                });
            modelBuilder.Entity<SystemRequirementRecommended>().HasData(
               new SystemRequirementRecommended()
               {
                   Id = Guid.NewGuid(),
                   OS = "Windows 10 64 Bit, Windows 8.1 64 Bit, Windows 8 64 Bit, Windows 7 64 Bit Service Pack 1",
                   Processor = " Intel Core i5 3470 @ 3.2GHz (4 CPUs) / AMD X8 FX-8350 @ 4GHz (8 CPUs)",
                   Memory = "8 GB RAM",
                   Graphics = "NVIDIA GTX 660 2GB / AMD HD 7870 2GB",
                   Storage = "72 GB available space",
                   AdditionalNotes = "",
                   GameID = 2,
                   Soundcard = "100% DirectX 10 compatible"
               });
            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "ADMIN",
                Description = "Administrator role"
            });
            var roleId2 = new Guid("52503F03-BDEA-4BF8-8A1A-D21AE2646483");
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = roleId2,
                Name = "User",
                NormalizedName = "USER",
                Description = "User role"
            });
            var hasher = new PasswordHasher<AppUser>();

            modelBuilder.Entity<UserAvatar>().HasData(new UserAvatar()
            {
                Id = Guid.NewGuid(),
                ImagePath = "imgnotfound.jpg",
                UserID = adminId
            });
            modelBuilder.Entity<UserThumbnail>().HasData(new UserThumbnail()
            {
                Id = Guid.NewGuid(),
                ImagePath = "imgnotfound.jpg",
                UserID=adminId
            });
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "Jayfend",
                NormalizedUserName = "JAYFEND",
                Email = "leenguyen1721@gmail.com",
                NormalizedEmail = "LEENGUYEN1721@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Luan@123"),
                SecurityStamp = string.Empty,
                FirstName = "Luan",
                LastName = "Nguyen Phung Le",
                Dob = new DateTime(2001, 07, 01)
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });
        }
    }
}