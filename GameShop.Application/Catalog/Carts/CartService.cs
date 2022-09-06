using GameShop.Data.EF;
using GameShop.Data.Entities;
using GameShop.ViewModels.Catalog.Carts;
using GameShop.ViewModels.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace GameShop.Application.Catalog.Carts
{
    public class CartService : ICartService
    {
        private readonly GameShopDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public CartService(GameShopDbContext context, UserManager<AppUser> useManager, SignInManager<AppUser> signInManager)
        {
            _context = context;
            _userManager = useManager;
            _signInManager = signInManager;
        }
        public async Task<ApiResult<bool>> AddToCart(string UserID, CartCreateRequest cartCreateRequest)
        {
            var getCart = await _context.Carts.FirstOrDefaultAsync(x => x.UserID.Equals(UserID) && x.Status.Equals("Active"));
            if(getCart != null)
            {
                var check = await _context.OrderedGames.FirstOrDefaultAsync(x => x.CartID == getCart.CartID && x.GameID == cartCreateRequest.GameID);
                if(check != null)
                {
                    return new ApiErrorResult<bool>("Bạn đã mua game này rồi");
                }
                else
                {   

                    OrderedGame newgame = new OrderedGame()
                    {
                        GameID = cartCreateRequest.GameID,
                        CartID = getCart.CartID,
                    };
                    _context.OrderedGames.Add(newgame);
                    await _context.SaveChangesAsync();
                    return new ApiSuccessResult<bool>();
                }
            }
            else
            {
                Cart newcart = new Cart()
                {
                    UserID = new Guid(UserID),
                    Status = Data.Enums.Status.Active,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    
                };
                OrderedGame newgame = new OrderedGame()
                {
                    GameID = cartCreateRequest.GameID,
                  Cart = newcart
                };
                _context.OrderedGames.Add(newgame);
                await _context.SaveChangesAsync();
                return new ApiSuccessResult<bool>();
            }
            
        }
    }
}
