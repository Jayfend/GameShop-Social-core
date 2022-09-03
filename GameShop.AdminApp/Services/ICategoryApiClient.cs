using GameShop.ViewModels.Catalog.Categories;
using GameShop.ViewModels.Catalog.Games;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameShop.AdminApp.Services
{
    public interface ICategoryApiClient
    {
        Task<GenreCreateRequest> GetById(int id);
        Task<List<CategoryViewModel>> GetAll();
    }
}
