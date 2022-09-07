using GameShop.Data.EF;
using GameShop.Data.Entities;
using GameShop.ViewModels.Catalog.Carts;
using GameShop.ViewModels.Catalog.Wishlists;
using GameShop.ViewModels.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Application.Catalog.Wishlists
{
    public class WishlistService : IWishlistService
    {
        private readonly GameShopDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public WishlistService(GameShopDbContext context, UserManager<AppUser> useManager, SignInManager<AppUser> signInManager)
        {
            _context = context;
            _userManager = useManager;
            _signInManager = signInManager;
        }
        public async Task<ApiResult<bool>> AddWishlist(string UserID, AddWishlistRequest addWishlistRequest)
        {
            var getCart = await _context.Wishlists.FirstOrDefaultAsync(x => x.UserID.ToString() == UserID);
            if (getCart != null)
            {
                var check = await _context.WishesGames.FirstOrDefaultAsync(x => x.ID == getCart.ID && x.GameID == addWishlistRequest.GameID);
                if (check != null)
                {
                    return new ApiErrorResult<bool>("Bạn đã thêm game này rồi");
                }
                else
                {

                    WishesGame newgame = new WishesGame()
                    {
                        GameID = addWishlistRequest.GameID,
                        WishID = getCart.ID,
                    };
                    _context.WishesGames.Add(newgame);
                    await _context.SaveChangesAsync();
                    return new ApiSuccessResult<bool>();
                }
            }
            else
            {
                Wishlist newcart = new Wishlist()
                {
                    UserID = new Guid(UserID),


                };
                WishesGame newgame = new WishesGame()
                {
                    GameID = addWishlistRequest.GameID,
                    Wishlist = newcart
                };
                _context.WishesGames.Add(newgame);
                await _context.SaveChangesAsync();
                return new ApiSuccessResult<bool>();
            }
        }
        public async Task<ApiResult<bool>> DeleteItem(string UserID, DeleteItemRequest orderItemDelete)
        {
            var orderitem = await _context.WishesGames
                .FirstOrDefaultAsync(x => x.Wishlist.UserID.ToString() == UserID && x.GameID == orderItemDelete.GameID);
            if (orderitem == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy game");
            }
            else
            {
                _context.WishesGames.Remove(orderitem);
                await _context.SaveChangesAsync();
                return new ApiSuccessResult<bool>();
            }
        }


        public async Task<ApiResult<WishlistItemResponse>> GetWishlist(string UserID)
        {
            var getCart = await _context.WishesGames.Where(x => x.Wishlist.UserID.ToString() == UserID)
                 .Select(x => new WishlistItemResponse()
                 {
                     Name = x.Game.GameName,
                     Price = x.Game.Price,
                     Discount = x.Game.Discount,

                 }).FirstOrDefaultAsync();
            if (getCart == null)
            {
                return new ApiErrorResult<WishlistItemResponse>("Không tìm thấy danh sách ước");
            }

            return new ApiSuccessResult<WishlistItemResponse>(getCart);
        }
    }
}
