using GameShop.ViewModels.Catalog.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameShop.AdminApp.Services
{
    public interface ICategoryApiClient
    {
        Task<List<CategoryViewModel>> GetById(int id);
        Task<List<CategoryViewModel>> GetAll();
    }
}
