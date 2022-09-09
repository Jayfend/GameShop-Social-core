using GameShop.ViewModels.Catalog.Carts;
using GameShop.ViewModels.Catalog.Checkouts;
using GameShop.ViewModels.Catalog.Games;
using GameShop.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Application.Catalog.Checkouts
{
    public interface ICheckoutService
    {
        Task<ApiResult<int>> CheckoutGame(string UserID);

        Task<PagedResult<GameViewModel>> GetPurchasedGames(string UserID, GetManageGamePagingRequest request);

        Task<ApiResult<CheckoutViewModel>> GetBill(int checkoutID);
    }
}