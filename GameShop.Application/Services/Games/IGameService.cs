using GameShop.ViewModels.Catalog.GameImages;
using GameShop.ViewModels.Catalog.Games;
using GameShop.ViewModels.Catalog.UserImages;
using GameShop.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Application.Services.Games
{
    public interface IGameService
    {
        Task<Guid> Create(GameCreateRequest request);

        Task<int> Update(Guid GameID, GameEditRequest request);

        Task<int> Delete(Guid GameID);

        Task<bool> UpdatePrice(Guid GameID, decimal newPrice);

        Task<string> Savefile(IFormFile file);

        //Task<PagedResult<GameViewModel>> GetAllbyGenreID(GetPublicGamePagingRequest request);

        Task<PagedResult<GameViewModel>> GetAll(GetManageGamePagingRequest request);

        Task<PagedResult<GameViewModel>> GetSaleGames(GetManageGamePagingRequest request);

        Task<PagedResult<GameViewModel>> GetAllPaging(GetManageGamePagingRequest request);

        Task<Guid> AddImage(Guid GameID, GameImageCreateRequest newimage);

        Task<int> RemoveImage(Guid imageId);

        Task<int> UpdateImage(Guid imageId, GameImageUpdateRequest Image);

        Task<List<GameImageViewModel>> GetListImages(Guid GameID);

        Task<GameViewModel> GetById(Guid GameID);

        Task<GameImageViewModel> GetImageById(Guid imageId);

        Task<ApiResult<bool>> CategoryAssign(Guid id, CategoryAssignRequest request);

        Task<PagedResult<GameBestSeller>> GetBestSeller(GetManageGamePagingRequest request);
        Task<bool> ActiveGameAsync(ActiveGameDTO req);
        
    }
}