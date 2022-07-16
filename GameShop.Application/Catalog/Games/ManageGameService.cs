using GameShop.Application.Catalog.Games.Dtos.Manage;
using GameShop.Application.Dtos;
using GameShop.Data.EF;
using GameShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Application.Catalog.Games
{
    public class ManageGameService : IManageGameService
    { private readonly GameShopDbContext _context;
        public ManageGameService(GameShopDbContext context)
        {
            _context = context;
        }

       

        public async Task<bool> Create(GameCreateRequest request)
        {
            var game = new Game()
            {
                GameName = request.GameName,
            };
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int GameID)
        {
            var game = await _context.Games.FindAsync(GameID);
            if(game == null)
            {
                return false;
            }
            else
            {
                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<List<GameViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<GameViewModel>> GetAllPaging(GetGamePagingRequest request)
        {
            var query = from p in _context.Games
                        join gig in _context.GameinGenres on p.GameID equals gig.GameID
                        join g in _context.Genres on gig.GenreID equals g.GenreID
                        select new { p, gig };
            // filter
            if (!String.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.p.GameName.Contains(request.Keyword));
                
            }
            
            if (request.GenreIDs.Count > 0)
            {
                query=query.Where(p=> request.GenreIDs.Contains(p.gig.GenreID));
            }
            //paging
            int totalrow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1)*request.PageSize)
                .Take(request.PageSize)
                .Select(x=>new GameViewModel()
                {
                    GameID = x.p.GameID,
                    GameName=x.p.GameName,
                    Gameplay=x.p.Gameplay,
                    Price=x.p.Price,
                    Discount=x.p.Discount,
                    Description=x.p.Description,
                    GenreID = x.gig.GenreID,
                    CreatedDate= x.p.CreatedDate,
                    UpdatedDate = x.p.UpdatedDate
                }).ToListAsync();
            //select and projection
            var pagedResult = new PagedResult<GameViewModel>()
            {
                TotalRecord = totalrow,
                Items = data
            };
            return pagedResult;

        }

        public async Task<bool> Update(GameEditRequest request)
        {
            var game = await _context.Games.FindAsync(request.GameID);
            if(game == null)
            {
                return false;
            }
            else
            { game.GameName = request.GameName;
              
                game.Discount = request.Discount;
                game.Description = request.Description;
                game.Gameplay = request.Gameplay;
                game.UpdatedDate = DateTime.Now;
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> UpdatePrice(int GameID, decimal newPrice)
        {
            var game = await _context.Games.FindAsync(GameID);
            if (game == null)
            {
                return false;
            }
            else
            {
                game.Price = newPrice;
                 await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}
