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
using GameShop.ViewModels.Common;
using GameShop.Data.Entities;

namespace GameShop.Application.Catalog.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly GameShopDbContext _context;

        public CategoryService(GameShopDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> CreateCategory(CreateCategoryRequest request)
        {
            var check = await _context.Genres.FirstOrDefaultAsync(x => x.GenreName.Contains(request.GenreName.Trim()));
            if (check != null)
            {
                return new ApiErrorResult<bool>("Thể loại đã tồn tại");
            }
            else
            {
                var newGenre = new Genre()
                {
                    GenreName = request.GenreName
                };
                await _context.Genres.AddAsync(newGenre);
                await _context.SaveChangesAsync();
                return new ApiSuccessResult<bool>();
            }
        }

        public async Task<ApiResult<bool>> EditCategory(EditCategoryRequest request)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == request.GenreID);
            if (genre == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy thể loại");
            }
            else
            {
                genre.GenreName = request.GenreName.Trim();
                _context.Genres.Update(genre);
                await _context.SaveChangesAsync();
                return new ApiSuccessResult<bool>();
            }
        }

        public async Task<List<CategoryViewModel>> GetAll()
        {
            var query = await _context.Genres.Select(x => new CategoryViewModel()
            {
                Id = x.Id,
                Name = x.GenreName,
            }).ToListAsync();
            return query;
        }

        public async Task<CategoryViewModel> GetById(Guid id)
        {
            var query = await _context.Genres.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (query != null)
            {
                CategoryViewModel genre = new CategoryViewModel()
                {
                    Name = query.GenreName,
                    Id = query.Id
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