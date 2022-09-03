using GameShop.Data.EF;
using GameShop.ViewModels.Catalog.Categories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using GameShop.ViewModels.Catalog.Games;

namespace GameShop.Application.Catalog.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly GameShopDbContext _context;

        public CategoryService(GameShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryViewModel>> GetAll()
        {
            var query = await _context.Genres.Select(x => new CategoryViewModel()
            {
                Id = x.GenreID,
                Name = x.GenreName,
                
            }).ToListAsync();
            return query;
        }

        public async Task<GenreCreateRequest> GetById(int id)
        {
            var query = await _context.Genres.Where(x => x.GenreID == id).FirstOrDefaultAsync();
            if(query != null)
            {
                GenreCreateRequest genre = new GenreCreateRequest()
                {
                    GenreName = query.GenreName,
                    GenreID = query.GenreID
                };
                return genre;
            }
            else
            {
                return null;
            }
          
        }
    }
}
