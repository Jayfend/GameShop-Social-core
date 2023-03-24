using GameShop.Data.EF;
using GameShop.Data.Entities;
using GameShop.ViewModels.Catalog.Charts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Application.Services.Charts
{
    public class ChartService : IChartService
    {
        private readonly GameShopDbContext _context;

        public ChartService(GameShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<GameBuyCountModel>> GameStatisticalByMonthAndYear(int Year, int Month, int take)
        {
            if (Year < 2022 || Year > DateTime.Today.Year)
            {
                Year = DateTime.Today.Year;
            }
            if (Month < 1 || Month > 12)
            {
                Month = DateTime.Today.Month;
            }
            var listCheckout = await _context.Checkouts
                .Where(x => x.Purchasedate.Year == Year && x.Purchasedate.Month == Month).Include(x => x.SoldGames).ToListAsync();
            var listGames = await _context.Games.Select(x => new GameBuyCountModel
            {
                Name = x.GameName,
                Id = x.Id,
                BuyCount = 0,
                Total = 0
            }).ToListAsync();
            if (take < 0 || take > listGames.Count)
            {
                take = listGames.Count;
            }
            var listSoldGame = new List<SoldGame>();
            foreach (var checkout in listCheckout)
            {
                var soldgames = checkout.SoldGames;
                foreach (var soldgame in soldgames)
                {
                    listSoldGame.Add(soldgame);
                }
            }
            foreach (var game in listGames)
            {
                foreach (var soldgame in listSoldGame)
                {
                    decimal totalprice = 0;
                    if (game.Id == soldgame.GameID)
                    {
                        game.BuyCount++;
                        totalprice = totalprice + (soldgame.Price - soldgame.Price * soldgame.Discount / 100);
                        game.Total += totalprice;
                    }
                }
            }
            return listGames.OrderByDescending(x => x.BuyCount).Take(take).ToList();
        }

        public async Task<List<GameBuyCountModel>> GameStatisticalByMonthAndYearSortbyTotal(int Year, int Month, int take)
        {
            if (Year < 2022 || Year > DateTime.Today.Year)
            {
                Year = DateTime.Today.Year;
            }
            if (Month < 1 || Month > 12)
            {
                Month = DateTime.Today.Month;
            }
            var listCheckout = await _context.Checkouts
                .Where(x => x.Purchasedate.Year == Year && x.Purchasedate.Month == Month).Include(x => x.SoldGames).ToListAsync();
            var listGames = await _context.Games.Select(x => new GameBuyCountModel
            {
                Name = x.GameName,
                Id = x.Id,
                BuyCount = 0,
                Total = 0
            }).ToListAsync();
            if (take < 0 || take > listGames.Count)
            {
                take = listGames.Count;
            }
            var listSoldGame = new List<SoldGame>();
            foreach (var checkout in listCheckout)
            {
                var soldgames = checkout.SoldGames;
                foreach (var soldgame in soldgames)
                {
                    listSoldGame.Add(soldgame);
                }
            }
            foreach (var game in listGames)
            {
                foreach (var soldgame in listSoldGame)
                {
                    decimal totalprice = 0;
                    if (game.Id == soldgame.GameID)
                    {
                        game.BuyCount++;
                        totalprice = totalprice + (soldgame.Price - soldgame.Price * soldgame.Discount / 100);
                        game.Total += totalprice;
                    }
                }
            }
            return listGames.OrderByDescending(x => x.Total).Take(take).ToList();
        }

        public async Task<List<GameBuyCountModel>> GameTotalPurchased()
        {
            var listGames = await _context.Games.Select(x => new GameBuyCountModel
            {
                Name = x.GameName,
                Id = x.Id,
                BuyCount = 0,
                Total = 0
            }).ToListAsync();
            var soldgames = await _context.SoldGames.ToListAsync();
            foreach (var game in listGames)
            {
                foreach (var soldgame in soldgames)
                {
                    decimal totalprice = 0;
                    if (game.Id == soldgame.GameID)
                    {
                        game.BuyCount++;
                        totalprice = totalprice + (soldgame.Price - soldgame.Price * soldgame.Discount / 100);
                        game.Total += totalprice;
                    }
                }
            }
            return listGames;
        }

        public async Task<decimal> TotalProfit()
        {
            var listCheckout = await _context.Checkouts.ToListAsync();
            decimal total = 0;
            foreach (var checkout in listCheckout)
            {
                total += checkout.TotalPrice;
            }
            return total;
        }
    }
}