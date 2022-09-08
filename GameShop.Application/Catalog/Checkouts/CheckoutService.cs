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
                    total = total + item.Price;
                }
                Checkout newCheckout = new Checkout()
                {
                    Cart = getCart,
                    Purchasedate = DateTime.Now,
                    TotalPrice = total,
                    Username = user.UserName
                };

                getCart.Status = (Status)0;
                _context.Carts.Update(getCart);
                _context.Checkouts.Add(newCheckout);

                await _context.SaveChangesAsync();

                return new ApiSuccessResult<int>(newCheckout.ID);
            }
        }

        public async Task<ApiResult<CheckoutViewModel>> GetBill(int checkoutID)
        {
            var bill = await _context.Checkouts.Where(x => x.ID == checkoutID).Select(x => new CheckoutViewModel()
            {
                CartID = x.CartID,
                TotalPrice = x.TotalPrice,
                Purchasedate = x.Purchasedate,
                Username = x.Username,
                Listgame = new List<GameViewModel>()
            }).FirstOrDefaultAsync();
            var game = await _context.OrderedGames.Where(x => x.CartID == bill.CartID).Select(x => new GameViewModel()
            {
                CreatedDate = x.Game.CreatedDate,
                Name = x.Game.GameName,
                Description = x.Game.Description,
                Gameplay = x.Game.Gameplay,
                Discount = x.Game.Discount,
                Price = x.Game.Price,
            }).ToListAsync();
            bill.Listgame = game;

            if (bill == null)
            {
                return new ApiErrorResult<CheckoutViewModel>("Không tìm thấy bill");
            }
            else
            {
                return new ApiSuccessResult<CheckoutViewModel>(bill);
            }
        }
    }
}