﻿using GameShop.Data.EF;
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

namespace GameShop.Application.Services.Wishlists
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

        public async Task<ApiResult<bool>> AddWishlist(Guid UserID, AddWishlistRequest addWishlistRequest)
        {
            var getCart = await _context.Wishlists.FirstOrDefaultAsync(x => x.UserID == UserID);
            if (getCart != null)
            {
                var orderedgames = await _context.OrderedGames.ToListAsync();
                var bills = await _context.Checkouts.Where(x => x.Cart.UserID == UserID).Select(y => y.CartID).ToListAsync();
                if (bills.Count > 0)
                {
                    foreach (var item in bills)
                    {
                        var checkbuy = orderedgames.FirstOrDefault(x => x.CartID == item && x.GameID == addWishlistRequest.GameID);
                        if (checkbuy != null)
                        {
                            return new ApiErrorResult<bool>("Bạn đã mua game này rồi");
                        }
                    }
                }
                var check = await _context.WishesGames.FirstOrDefaultAsync(x => x.Id == getCart.Id && x.GameID == addWishlistRequest.GameID);
                if (check != null)
                {
                    return new ApiErrorResult<bool>("Bạn đã thêm game này rồi");
                }
                else
                {
                    WishesGame newgame = new WishesGame()
                    {
                        GameID = addWishlistRequest.GameID,
                        WishID = getCart.Id,
                        CreatedDate = DateTime.Now
                    };
                    _context.WishesGames.Add(newgame);
                    await _context.SaveChangesAsync();
                    return new ApiSuccessResult<bool>();
                }
            }
            else
            {
                var orderedgames = await _context.OrderedGames.ToListAsync();
                var bills = await _context.Checkouts.Where(x => x.Cart.UserID == UserID).Select(y => y.CartID).ToListAsync();
                if (bills.Count > 0)
                {
                    foreach (var item in bills)
                    {
                        var checkbuy = orderedgames.FirstOrDefault(x => x.CartID == item && x.GameID == addWishlistRequest.GameID);
                        if (checkbuy != null)
                        {
                            return new ApiErrorResult<bool>("Bạn đã mua game này rồi");
                        }
                    }
                }
                Wishlist newcart = new Wishlist()
                {
                    UserID = UserID
                };
                WishesGame newgame = new WishesGame()
                {
                    GameID = addWishlistRequest.GameID,
                    Wishlist = newcart,
                    CreatedDate = DateTime.Now
                };
                _context.WishesGames.Add(newgame);
                await _context.SaveChangesAsync();
                return new ApiSuccessResult<bool>();
            }
        }

        public async Task<ApiResult<bool>> DeleteItem(Guid UserID, DeleteItemRequest orderItemDelete)
        {
            var orderitem = await _context.WishesGames
                .FirstOrDefaultAsync(x => x.Wishlist.UserID == UserID && x.GameID == orderItemDelete.GameID);
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

        public async Task<ApiResult<List<WishlistItemResponse>>> GetWishlist(Guid UserID)
        {
            var getCart = await _context.WishesGames.Where(x => x.Wishlist.UserID == UserID)
                 .Select(x => new WishlistItemResponse()
                 {
                     GameID = x.GameID,
                     Name = x.Game.GameName,
                     Price = x.Game.Price,
                     Discount = x.Game.Discount,
                     ImageList = new List<string>(),
                     AddedDate = x.CreatedDate,
                     GenreName = new List<string>(),
                     GenreIds = x.Game.GameInGenres.Select(y => y.GenreID).ToList(),
                 }).ToListAsync();
            var genres = _context.Genres.AsQueryable();

            foreach (var item in getCart)
            {
                foreach (var genre in item.GenreIds)
                {
                    var name = genres.Where(x => x.Id == genre).Select(y => y.GenreName).FirstOrDefault();
                    item.GenreName.Add(name);
                }
            }
            var thumbnailimage = _context.GameImages.AsQueryable();
            foreach (var item in getCart)
            {
                var listgame = thumbnailimage.Where(x => x.GameID == item.GameID).Select(y => y.ImagePath).ToList();
                item.ImageList = listgame;
            }

            if (getCart == null)
            {
                return new ApiErrorResult<List<WishlistItemResponse>>("Không tìm thấy danh sách ước");
            }

            return new ApiSuccessResult<List<WishlistItemResponse>>(getCart);
        }
    }
}