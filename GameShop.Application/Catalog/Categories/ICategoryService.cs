using GameShop.ViewModels.Catalog.Categories;
using GameShop.ViewModels.Catalog.Games;
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
    }
}
