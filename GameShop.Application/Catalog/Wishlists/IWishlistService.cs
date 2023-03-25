using GameShop.ViewModels.Catalog.Carts;
using GameShop.ViewModels.Catalog.Wishlists;
using GameShop.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Application.Catalog.Wishlists
{
    public interface IWishlistService
    {
        Task<ApiResult<bool>> AddWishlist(string UserID, AddWishlistRequest addWishlistRequest);

        Task<ApiResult<List<WishlistItemResponse>>> GetWishlist(string UserID);

        Task<ApiResult<bool>> DeleteItem(string UserID, DeleteItemRequest orderItemDelete);
    }
}