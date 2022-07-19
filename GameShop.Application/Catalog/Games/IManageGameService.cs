using GameShop.ViewModels.Catalog.Games;
using GameShop.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Application.Catalog.Games
{
    public interface IManageGameService
    {
        Task<bool> Create(GameCreateRequest request);
        Task<bool> Update(GameEditRequest request);
        Task<bool> Delete(int GameID);
        Task<bool> UpdatePrice(int GameID, Decimal newPrice);
        Task<List<GameViewModel>> GetAll();
        Task<string> Savefile(IFormFile file);
        Task<PagedResult<GameViewModel>> GetAllPaging(GetManageGamePagingRequest request);
        Task<bool> AddImages(int GameID, List<IFormFile> files);
        Task<int> RemoveImage(int ImageID);
        Task<int> UpdateImage(int ImageID, string caption, bool isDefault);
       
    }
}
