using GameShop.ViewModels.Catalog.Carts;
using GameShop.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Application.Services.Carts
{
    public interface ICartService
    {
        Task<ApiResult<bool>> AddToCart(string UserID, CartCreateRequest cartCreateRequest);

        Task<ApiResult<List<OrderItemResponse>>> GetCart(string UserID);

        Task<ApiResult<bool>> DeleteItem(string UserID, OrderItemDelete orderItemDelete);
    }
}