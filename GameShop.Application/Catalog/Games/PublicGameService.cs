using GameShop.Data.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using GameShop.ViewModels.Catalog.Games;
using GameShop.ViewModels.Common;

namespace GameShop.Application.Catalog.Games
{
    public class PublicGameService : IPublicGameService
    {
        private readonly GameShopDbContext _context;
        public PublicGameService(GameShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<GameViewModel>> GetAll()
        {

            var data = await _context.Games.Select(x => new GameViewModel()
            {
                GameID = x.GameID,
                GameName = x.GameName,
                Gameplay = x.Gameplay,
                Price = x.Price,
                Discount = x.Discount,
                GenreIDs=new List<int>(),
                Description = x.Description,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate
            }).ToListAsync();
             foreach(var game in data)
            {
                var genres = await _context.GameinGenres.Where(x => x.GameID == game.GameID).ToListAsync();
                foreach(var genre in genres)
                {
                    game.GenreIDs.Add(genre.GenreID);
                }
            }
            return data;
        }

        public async Task<PagedResult<GameViewModel>> GetAllbyGenreID(GetPublicGamePagingRequest request)
        {

            var query = from p in _context.Games
                        join gig in _context.GameinGenres on p.GameID equals gig.GameID
                        join g in _context.Genres on gig.GenreID equals g.GenreID
                        select new { p, gig };
            // filter
            if (request.GenreID.HasValue && request.GenreID.Value>0)
            {
                query = query.Where(x => x.gig.GenreID==request.GenreID);

            }

            //paging
            int totalrow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new GameViewModel()
                {
                    GameID = x.p.GameID,
                    GameName = x.p.GameName,
                    Gameplay = x.p.Gameplay,
                    Price = x.p.Price,
                    Discount = x.p.Discount,
                    GenreIDs = new List<int>(),
                    Description = x.p.Description,
                    CreatedDate = x.p.CreatedDate,
                    UpdatedDate = x.p.UpdatedDate
                }).ToListAsync();
            foreach (var game in data)
            {
                var genres = await _context.GameinGenres.Where(x => x.GameID == game.GameID).ToListAsync();
                foreach (var genre in genres)
                {
                    game.GenreIDs.Add(genre.GenreID);
                }
            }
            //select and projection
            var pagedResult = new PagedResult<GameViewModel>()
            {
                TotalRecord = totalrow,
                Items = data
            };
            return pagedResult;

        }
      
    }
}
