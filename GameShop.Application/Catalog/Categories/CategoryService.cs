using GameShop.Data.EF;
using GameShop.ViewModels.Catalog.Categories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public async Task<List<CategoryViewModel>> GetById(int id)
        {
            var query = _context.Genres.Where(x => x.GenreID == id);
            return await query.Select(x => new CategoryViewModel()
            {
                Id = x.GenreID,
                Name = x.GenreName,
            }).ToListAsync();
        }
    }
}
