using GameShop.Data.Entities;
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
                    GenreID = 1,
                    GenreName = "Action"
                });
            modelBuilder.Entity<Genre>().HasData(
               new Genre()
               {
                   GenreID = 2,
                   GenreName = "Open-World"
               });
            modelBuilder.Entity<Genre>().HasData(
               new Genre()
               {
                   GenreID = 3,
                   GenreName = "Multiplayer"
               });

            modelBuilder.Entity<Game>().HasData(
                new Game()
                {   GameID = 1,
                    GameName = "Grand Theft Auto V",
                    Price = 250000,
                    Discount = 0,
                    Description = "The best game in the world",
                    Gameplay = "Destroy the city",
                    CreatedDate = DateTime.Now,
                    Status = Enums.Status.Active,
                    UpdatedDate=DateTime.Now,

                });
            modelBuilder.Entity<Game>().HasData(
               new Game()
               {
                   GameID = 2,
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
                new GameinGenre() { GenreID = 1, GameID = 1},
                new GameinGenre() { GenreID=2, GameID=1}
            );
            modelBuilder.Entity<GameinGenre>().HasData(
                new GameinGenre() { GenreID = 3, GameID = 2 },
                new GameinGenre() { GenreID = 2, GameID = 2 }
            );
            modelBuilder.Entity<SystemRequirementMin>().HasData(
                new SystemRequirementMin()
                {   SRMID = 1,
                    OS= "Windows 10 64 Bit, Windows 8.1 64 Bit, Windows 8 64 Bit, Windows 7 64 Bit Service Pack 1",
                    Processor = "Intel Core 2 Quad CPU Q6600 @ 2.40GHz (4 CPUs) / AMD Phenom 9850 Quad-Core Processor (4 CPUs) @ 2.5GHz",
                    Memory= "4 GB RAM",
                    Graphics= "NVIDIA 9800 GT 1GB / AMD HD 4870 1GB (DX 10, 10.1, 11)",
                    Storage= "72 GB available space",
                    AdditionalNotes="",
                    GameID=1,
                    Soundcard= "100% DirectX 10 compatible"



                });
            modelBuilder.Entity<SystemRequirementMin>().HasData(
              new SystemRequirementMin()
              {
                  SRMID = 2,
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
                {   SRRID = 1,
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
                   SRRID = 2,
                   OS = "Windows 10 64 Bit, Windows 8.1 64 Bit, Windows 8 64 Bit, Windows 7 64 Bit Service Pack 1",
                   Processor = " Intel Core i5 3470 @ 3.2GHz (4 CPUs) / AMD X8 FX-8350 @ 4GHz (8 CPUs)",
                   Memory = "8 GB RAM",
                   Graphics = "NVIDIA GTX 660 2GB / AMD HD 7870 2GB",
                   Storage = "72 GB available space",
                   AdditionalNotes = "",
                   GameID = 2,
                   Soundcard = "100% DirectX 10 compatible"

               });
        }
    }
}
