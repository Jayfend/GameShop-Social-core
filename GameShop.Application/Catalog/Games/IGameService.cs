using GameShop.ViewModels.Catalog.GameImages;
using GameShop.ViewModels.Catalog.Games;
using GameShop.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Application.Catalog.Games
{
    public interface IGameService
    {
        Task<int> Create(GameCreateRequest request);

        Task<int> Update(GameEditRequest request);

        Task<int> Delete(int GameID);

        Task<bool> UpdatePrice(int GameID, Decimal newPrice);

        Task<string> Savefile(IFormFile file);

        Task<PagedResult<GameViewModel>> GetAllbyGenreID(GetPublicGamePagingRequest request);

        Task<List<GameViewModel>> GetAll();

        Task<PagedResult<GameViewModel>> GetAllPaging(GetManageGamePagingRequest request);

        Task<int> AddImage(int GameID, GameImageCreateRequest newimage);

        Task<int> RemoveImage(int ImageID);

        Task<int> UpdateImage(int ImageID, GameImageUpdateRequest Image);

        Task<List<GameImageViewModel>> GetListImages(int GameID);

        Task<GameViewModel> GetById(int GameID);

        Task<GameImageViewModel> GetImageById(int ImageID);
    }
}