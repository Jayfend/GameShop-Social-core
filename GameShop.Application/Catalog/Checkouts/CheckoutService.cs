using GameShop.Data.EF;
using GameShop.Data.Entities;
using GameShop.Data.Enums;
using GameShop.ViewModels.Catalog.Carts;
using GameShop.ViewModels.Catalog.Checkouts;
using GameShop.ViewModels.Catalog.Games;
using GameShop.ViewModels.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Application.Catalog.Checkouts
{
    public class CheckoutService : ICheckoutService
    {
        private readonly GameShopDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public CheckoutService(GameShopDbContext context, UserManager<AppUser> useManager, SignInManager<AppUser> signInManager)
        {
            _context = context;
            _userManager = useManager;
            _signInManager = signInManager;
        }

        public async Task<ApiResult<int>> CheckoutGame(string UserID)
        {
            var user = await _userManager.FindByIdAsync(UserID);
            Decimal total = 0;
            var getCart = await _context.Carts.FirstOrDefaultAsync(x => x.UserID.ToString() == UserID && x.Status.Equals((Status)1));
            if (getCart == null)
            {
                return new ApiErrorResult<int>("Không tìm thấy giỏ hàng");
            }
            else
            {
                var gamelist = await _context.OrderedGames.Where(x => x.CartID == getCart.CartID).Select(y => y.Game).ToListAsync();
                foreach (var item in gamelist)
                {
                    total = total + (item.Price - (item.Price * (item.Discount / 100)));
                }
                Checkout newCheckout = new Checkout()
                {
                    Cart = getCart,
                    Purchasedate = DateTime.Now,
                    TotalPrice = total,
                    Username = user.UserName,
                };
                //_context.Checkouts.Add(newCheckout);
                foreach (var item in gamelist)
                {
                    var soldgame = new SoldGame()
                    {
                        GameID = item.GameID,
                        GameName = item.GameName,
                        Discount = item.Discount,
                        Price = item.Price,
                        GameImages = item.GameImages,
                        Checkout = newCheckout
                    };
                    await _context.SoldGames.AddAsync(soldgame);
                }

                var listgame = await _context.OrderedGames.Where(x => x.CartID == getCart.CartID).ToListAsync();
                foreach (var game in listgame)
                {
                    var wishedgames = await _context.WishesGames.FirstOrDefaultAsync(x => x.GameID == game.GameID && x.Wishlist.UserID.ToString() == UserID);
                    if (wishedgames != null)
                    {
                        _context.WishesGames.Remove(wishedgames);
                    }
                }
                getCart.Status = (Status)0;
                _context.Carts.Update(getCart);

                await _context.SaveChangesAsync();

                return new ApiSuccessResult<int>(newCheckout.ID);
            }
        }

        public async Task<ApiResult<CheckoutViewModel>> GetBill(int checkoutID)
        {
            var bill = await _context.Checkouts.Include(x => x.SoldGames).FirstOrDefaultAsync(x => x.ID == checkoutID);
            if (bill != null)
            {
                var newbill = new CheckoutViewModel()
                {
                    CartID = bill.CartID,
                    TotalPrice = bill.TotalPrice,
                    Purchasedate = bill.Purchasedate,
                    Username = bill.Username,
                    Listgame = new List<GameViewModel>()
                };
                //var game = await _context.OrderedGames.Where(x => x.CartID == bill.CartID).Select(x => new GameViewModel()
                //{
                //    CreatedDate = x.Game.CreatedDate,
                //    Name = x.Game.GameName,
                //    Description = x.Game.Description,
                //    Gameplay = x.Game.Gameplay,
                //    Discount = x.Game.Discount,
                //    Price = x.Game.Price,
                //}).ToListAsync();
                foreach (var game in bill.SoldGames)
                {
                    var soldgame = new GameViewModel()
                    {
                        GameID = game.GameID,
                        Name = game.GameName,
                        Price = game.Price,
                        Discount = game.Discount,
                    };
                    newbill.Listgame.Add(soldgame);
                }
                //newbill.Listgame = game;
                return new ApiSuccessResult<CheckoutViewModel>(newbill);
            }
            else
            {
                return new ApiErrorResult<CheckoutViewModel>("Không tìm thấy bill");
            }
        }

        public async Task<PagedResult<GameViewModel>> GetPurchasedGames(string UserID, GetManageGamePagingRequest request)
        {
            var query = _context.Checkouts
                .Where(x => x.Cart.UserID.ToString() == UserID).SelectMany(x => x.SoldGames).AsQueryable();
            //query = query.Where(x => x.Cart.UserID.ToString() == UserID && x.Cart.Status.Equals((Status)0));
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.GameName.Contains(request.Keyword));
            }

            //if (request.GenreID != null)
            //{
            //    query = query.Where(x => x.GameInGenres.Any(x => x.GenreID == request.GenreID));
            //}

            int totalrow = await query.CountAsync();
            var data = await query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new GameViewModel()
                {
                    GameID = x.GameID,
                    Name = x.GameName,
                    Price = x.Price,
                    Discount = x.Discount,
                    ListImage = new List<string>()
                }).ToListAsync();
            //var genres = _context.Genres.AsQueryable();
            //foreach (var item in data)
            //{
            //    foreach (var genre in item.GenreIDs)
            //    {
            //        var name = genres.Where(x => x.GenreID == genre).Select(y => y.GenreName).FirstOrDefault();
            //        item.GenreName.Add(name);
            //    }
            //}
            var thumbnailimage = _context.GameImages.AsQueryable();
            foreach (var item in data)
            {
                var listgame = thumbnailimage.Where(x => x.GameID == item.GameID).Select(y => y.ImagePath).ToList();
                item.ListImage = listgame;
            }
            var pagedResult = new PagedResult<GameViewModel>()
            {
                TotalRecords = totalrow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return pagedResult;
        }
    }
}