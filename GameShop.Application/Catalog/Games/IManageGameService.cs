using GameShop.Application.Catalog.Games.Dtos.Manage;
using GameShop.Application.Dtos;
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
        Task<PagedResult<GameViewModel>> GetAllPaging(GetGamePagingRequest request);
    }
}
