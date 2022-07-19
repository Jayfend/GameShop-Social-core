using GameShop.Data.EF;
using GameShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using GameShop.ViewModels.Catalog.Games;
using GameShop.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;
using GameShop.Application.Common;

namespace GameShop.Application.Catalog.Games
{
    public class ManageGameService : IManageGameService
    { private readonly GameShopDbContext _context;
        private readonly IStorageService _storageService;
        public ManageGameService(GameShopDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

       

        public async Task<bool> Create(GameCreateRequest request)
        {
            var game = new Game()
            {
                GameName = request.GameName,
               
            };
            if(request.ThumbnailImage != null)
            {
                game.GameImages = new List<GameImage>()
                {
                     new GameImage()
                     {
                         Caption="Thumbnail Image",
                         CreatedDate = DateTime.Now,
                         Filesize = request.ThumbnailImage.Length,
                         ImagePath=  await this.Savefile(request.ThumbnailImage),
                         isDefault = true,
                         SortOrder = 1,

                     }
                };
            }
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int GameID)
        {
            var game = await _context.Games.FindAsync(GameID);
            if(game == null)
            {
                return false;
            }
            else
            {
                var thumbnailImages = _context.GameImages.Where(i=> i.GameID == GameID);
                  foreach(var item in thumbnailImages)
                {
                    await _storageService.DeleteFileAsync(item.ImagePath);
                          _context.GameImages.Remove(item);
                }
                    
               
                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<List<GameViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<GameViewModel>> GetAllPaging(GetManageGamePagingRequest request)
        {
            var query = from p in _context.Games
                        join gig in _context.GameinGenres on p.GameID equals gig.GameID
                        join g in _context.Genres on gig.GenreID equals g.GenreID
                        select new { p, gig };
            // filter
            if (!String.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.p.GameName.Contains(request.Keyword));
                
            }
            
            if (request.GenreIDs.Count > 0)
            {
                query=query.Where(p=> request.GenreIDs.Contains(p.gig.GenreID));
            }
            //paging
            int totalrow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1)*request.PageSize)
                .Take(request.PageSize)
                .Select(x=>new GameViewModel()
                {
                    GameID = x.p.GameID,
                    GameName=x.p.GameName,
                    Gameplay=x.p.Gameplay,
                    Price=x.p.Price,
                    Discount=x.p.Discount,
                    Description=x.p.Description,
                    GenreID = x.gig.GenreID,
                    CreatedDate= x.p.CreatedDate,
                    UpdatedDate = x.p.UpdatedDate
                }).ToListAsync();
            //select and projection
            var pagedResult = new PagedResult<GameViewModel>()
            {
                TotalRecord = totalrow,
                Items = data
            };
            return pagedResult;

        }

        public async Task<bool> Update(GameEditRequest request)
        {
            var game = await _context.Games.FindAsync(request.GameID);
            if(game == null)
            {
                return false;
            }
            else
            { game.GameName = request.GameName;
              
                game.Discount = request.Discount;
                game.Description = request.Description;
                game.Gameplay = request.Gameplay;
                game.UpdatedDate = DateTime.Now;
                if (request.ThumbnailImage != null)
                {
                    var thumbnailImage = await _context.GameImages.FirstOrDefaultAsync(i => i.isDefault == true && i.GameID == request.GameID);
                  if(thumbnailImage != null)
                    {
                        thumbnailImage.Filesize = request.ThumbnailImage.Length;
                        thumbnailImage.ImagePath = await this.Savefile(request.ThumbnailImage);
                        _context.GameImages.Update(thumbnailImage);
                       

                    }
               
                }
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> UpdatePrice(int GameID, decimal newPrice)
        {
            var game = await _context.Games.FindAsync(GameID);
            if (game == null)
            {
                return false;
            }
            else
            {
                game.Price = newPrice;
                 await _context.SaveChangesAsync();
                return true;
            }
        }
        public async Task<string> Savefile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var filename = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(),filename);
            return filename;
        }

        public Task<bool> AddImages(int GameID, List<IFormFile> files)
        {
            throw new NotImplementedException();
        }

        public Task<int> RemoveImage(int ImageID)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateImage(int ImageID, string caption, bool isDefault)
        {
            throw new NotImplementedException();
        }
    }
}
