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
            modelBuilder.Entity<GameinGenre>().HasData(
                new GameinGenre() { GenreID = 1, GameID = 1},
                new GameinGenre() { GenreID=2,GameID=1}
            );
           
        }
    }
}
