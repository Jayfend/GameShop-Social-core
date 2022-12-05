using GameShop.ViewModels.Catalog.Categories;
using GameShop.ViewModels.Catalog.Games;
using GameShop.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Application.Catalog.Categories
{
    public interface ICategoryService
    {
        Task<CategoryViewModel> GetById(int id);

        Task<List<CategoryViewModel>> GetAll();

        Task<ApiResult<bool>> CreateCategory(CreateCategoryRequest request);

        Task<ApiResult<bool>> EditCategory(EditCategoryRequest request);
    }
}