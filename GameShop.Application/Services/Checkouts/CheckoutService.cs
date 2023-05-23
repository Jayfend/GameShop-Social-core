using GameShop.Data.EF;
using GameShop.Data.Entities;
using GameShop.ViewModels.Catalog.Carts;
using GameShop.ViewModels.Catalog.Checkouts;
using GameShop.ViewModels.Catalog.Games;
using GameShop.ViewModels.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Security.Policy;
using GameShop.Utilities.Exceptions;
using Newtonsoft.Json;
using StackExchange.Redis;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using GameShop.Utilities.Configurations;
using GameShop.Utilities.Redis;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace GameShop.Application.Services.Checkouts
{
    public class CheckoutService : ICheckoutService
    {
        private readonly GameShopDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        readonly IRedisUtil _redisUtil;
        readonly RedisConfig _redisConfig;

        public CheckoutService(
            GameShopDbContext context
            , UserManager<AppUser> useManager
            , SignInManager<AppUser> signInManager
            , IRedisUtil redisUtil
            , IOptions<RedisConfig> redisConfig)
        {
            _context = context;
            _userManager = useManager;
            _signInManager = signInManager;
            _redisUtil = redisUtil;
            _redisConfig = redisConfig.Value;
        }

        public async Task<ApiResult<Guid>> CheckoutGame(Guid UserID)
        {
            var user = await _userManager.FindByIdAsync(UserID.ToString());
            if(user.Email == null)
            {
                return new ApiErrorResult<Guid>("Vui lòng cập nhật Email trước khi mua hàng");
            }
            decimal total = 0;
            var getCart = await _context.Carts.FirstOrDefaultAsync(x => x.UserID == UserID && x.Status == true);
            if (getCart == null)
            {
                return new ApiErrorResult<Guid>("Không tìm thấy giỏ hàng");
            }
            else
            {
                var gamelist = new List<Game>();
                try
                {
                    gamelist = await _context.OrderedGames.Where(x => x.CartID == getCart.Id).Select(y => y.Game).ToListAsync();
                }
                catch (Exception ex)
                {
                }
                foreach (var item in gamelist)
                {
                    total = total + (item.Price - item.Price * item.Discount / 100);
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
                    var image = _context.GameImages.Where(x => x.GameID == item.Id).Select(x => x.ImagePath).FirstOrDefault();
                    var soldgame = new SoldGame()
                    {
                        GameID = item.Id,
                        GameName = item.GameName,
                        Discount = item.Discount,
                        Price = item.Price,
                        ImagePath = image,
                        Checkout = newCheckout,
                        GameFile = item.FilePath,
                        CreatedDate = newCheckout.Purchasedate,
                    };
                    await _context.SoldGames.AddAsync(soldgame);
                }

                var listgame = await _context.OrderedGames.Where(x => x.CartID == getCart.Id).ToListAsync();
                foreach (var game in listgame)
                {
                    var wishedgames = await _context.WishesGames.FirstOrDefaultAsync(x => x.GameID == game.GameID && x.Wishlist.UserID == UserID);
                    if (wishedgames != null)
                    {
                        _context.WishesGames.Remove(wishedgames);
                    }
                }
                getCart.Status = false;
                _context.Carts.Update(getCart);

                await _context.SaveChangesAsync();
                var keyCodeList = new Dictionary<string,string>();
                foreach (var gameBought in gamelist)
                { var publisher = await _context.Publishers.Where(x => x.Id == gameBought.PublisherId).FirstOrDefaultAsync();
                    var keys =  await _redisUtil.HashGetAllAsync(string.Format(_redisConfig.DSMKey, publisher.Name, gameBought.GameName));
                   var keyList = new List<Data.Entities.Key>();

                    foreach(var key in keys)
                    {
                        var parsedKey = JsonConvert.DeserializeObject<Data.Entities.Key>(key);
                        keyList.Add(parsedKey);
                    }
                     var keyCode = keyList.Where(x => x.GameName == gameBought.GameName && x.Status == true).FirstOrDefault();
                    keyCode.Status = false;
                    keyCodeList.Add(gameBought.GameName, keyCode.KeyCode);
                    List<HashEntry> entries = new List<HashEntry>();
                    var hashKey = new HashEntry(keyCode.Id.ToString(), JsonConvert.SerializeObject(keyCode));
                    entries.Add(hashKey);
                    await _redisUtil.SetMultiAsync(string.Format(_redisConfig.DSMKey, publisher.Name, gameBought.GameName),entries.ToArray(),null);

                }
                var EmailFormat = "<table><thead><tr><th>Game Name</th><th>Key</th></thead>";
                foreach (var gameKey in keyCodeList)
                {
                    EmailFormat += $"<tr><td>{gameKey.Key}</td><td>{gameKey.Value}</td></tr>";
                }
                EmailFormat += "</table>";
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("stemgameshop@gmail.com");
                    mail.To.Add(user.Email);
                    mail.Subject = "Confirm Account";
                    mail.Body = $@"<html>
                      <body>
                      <p>Dear 
                    {user.UserName},</p>
                      <p>Thank for buying our games,here is your key code </p>
                        {EmailFormat}
                      <p>Sincerely,<br>-STEM</br></p>
                      </body>
                      </html>
                     ";
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential("stemgameshop@gmail.com", "tditidglubtzxojy");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
                return new ApiSuccessResult<Guid>(newCheckout.Id);
            }
        }

        public async Task<ApiResult<List<CheckoutViewModel>>> GetAllBill()
        {
            var listbill = new List<CheckoutViewModel>();
            var newbill = new CheckoutViewModel();
            var alluser = await _context.Users.ToListAsync();
            foreach (var user in alluser)
            {
                var checkouts = _context.Checkouts.Where(x => x.Cart.UserID == user.Id).ToList();

                foreach (var checkout in checkouts)
                {
                    var bill = await _context.Checkouts.Include(x => x.SoldGames).FirstOrDefaultAsync(x => x.Id == checkout.Id);
                    if (bill != null)
                    {
                        newbill = new CheckoutViewModel()
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
                                Id = game.GameID,
                                Name = game.GameName,
                                Price = game.Price,
                                Discount = game.Discount,
                                CreatedDate = game.CreatedDate
                            };
                            soldgame.ListImage.Add(game.ImagePath);
                            newbill.Listgame.Add(soldgame);
                        }
                        //newbill.Listgame = game;
                    }
                    else
                    {
                        return new ApiErrorResult<List<CheckoutViewModel>>("Không tìm thấy bill");
                    }

                    listbill.Add(newbill);
                }
            }
            return new ApiSuccessResult<List<CheckoutViewModel>>(listbill);
        }

        public async Task<ApiResult<CheckoutViewModel>> GetBill(Guid checkoutID)
        {
            var bill = await _context.Checkouts.Include(x => x.SoldGames).FirstOrDefaultAsync(x => x.Id == checkoutID);
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
                        Id = game.GameID,
                        Name = game.GameName,
                        Price = game.Price,
                        Discount = game.Discount,
                        CreatedDate = game.CreatedDate
                    };
                    soldgame.ListImage.Add(game.ImagePath);
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

        public async Task<PagedResult<GameViewModel>> GetPurchasedGames(Guid UserID, GetManageGamePagingRequest request)
        {
            var query = _context.Checkouts
                .Where(x => x.Cart.UserID == UserID).SelectMany(x => x.SoldGames).AsQueryable();
            if (!query.Any())
            {
                return new PagedResult<GameViewModel>()
                {
                    TotalRecords = 0,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    Items = null
                };
            }
            else
            {
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
                        Id = x.GameID,
                        Name = x.GameName,
                        Price = x.Price,
                        Discount = x.Discount,
                        ListImage = new List<string>() { x.ImagePath },
                        FileGame = x.GameFile,
                        CreatedDate = x.CreatedDate,
                        isActive = x.isActive
                        
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
                //var thumbnailimage = _context.GameImages.AsQueryable();
                //foreach (var item in data)
                //{
                //    var listgame = thumbnailimage.Where(x => x.GameID == item.GameID).Select(y => y.ImagePath).ToList();
                //    item.ListImage = listgame;
                //}
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
}