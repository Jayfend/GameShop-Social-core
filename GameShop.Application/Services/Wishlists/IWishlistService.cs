using GameShop.ViewModels.Catalog.Carts;
using GameShop.ViewModels.Catalog.Wishlists;
using GameShop.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Application.Services.Wishlists
{
    public interface IWishlistService
    {
        Task<ApiResult<bool>> AddWishlist(Guid UserID, AddWishlistRequest addWishlistRequest);

        Task<ApiResult<List<WishlistItemResponse>>> GetWishlist(Guid UserID);

        Task<ApiResult<bool>> DeleteItem(Guid UserID, DeleteItemRequest orderItemDelete);
    }
}