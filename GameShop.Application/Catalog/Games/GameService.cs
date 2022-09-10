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
using GameShop.Utilities.Exceptions;
using GameShop.ViewModels.Catalog.GameImages;
using GameShop.Data.Enums;
using Microsoft.AspNetCore.Mvc;
using GameShop.ViewModels.Catalog.UserImages;

namespace GameShop.Application.Catalog.Games
{
    public class GameService : IGameService
    {
        private readonly GameShopDbContext _context;
        private readonly IStorageService _storageService;

        public GameService(GameShopDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<int> Create(GameCreateRequest request)
        {
            var game = new Game()
            {
                GameName = request.GameName,
                Price = request.Price,
                Description = request.Description,
                Discount = request.Discount,
                Gameplay = request.Gameplay,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Status = (Status)request.Status
            };
            var genrelist = from g in _context.Genres select g;

            var genre = genrelist.FirstOrDefault(x => x.GenreID == request.Genre);

            var newgameingenre = new GameinGenre()
            {
                Game = game,
                Genre = genre
            };
            _context.GameinGenres.Add(newgameingenre);

            if (request.ThumbnailImage != null)
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
            if (request.SRR != null)
            {
                game.SystemRequirementRecommended = new SystemRequirementRecommended()
                {
                    OS = request.SRR.OS,
                    Processor = request.SRR.Processor,
                    Memory = request.SRR.Memory,
                    Graphics = request.SRR.Graphics,
                    Storage = request.SRR.Storage,
                    AdditionalNotes = request.SRR.AdditionalNotes,
                    Soundcard = request.SRR.Soundcard
                };
            }
            if (request.SRM != null)
            {
                game.SystemRequirementMin = new SystemRequirementMin()
                {
                    OS = request.SRM.OS,
                    Processor = request.SRM.Processor,
                    Memory = request.SRM.Memory,
                    Graphics = request.SRM.Graphics,
                    Storage = request.SRM.Storage,
                    AdditionalNotes = request.SRM.AdditionalNotes,
                    Soundcard = request.SRM.Soundcard
                };
            }

            _context.Games.Add(game);

            await _context.SaveChangesAsync();
            return game.GameID;
        }

        public async Task<int> Delete(int GameID)
        {
            var game = await _context.Games.FindAsync(GameID);
            if (game == null)
            {
                throw new GameShopException($"Can not find a game");
            }
            else
            {
                var thumbnailImages = _context.GameImages.Where(i => i.GameID == GameID);
                foreach (var item in thumbnailImages)
                {
                    await _storageService.DeleteFileAsync(item.ImagePath);
                    _context.GameImages.Remove(item);
                }

                _context.Games.Remove(game);
                return await _context.SaveChangesAsync();
            }
        }

        public async Task<PagedResult<GameViewModel>> GetAllPaging(GetManageGamePagingRequest request)
        {
            var query = _context.Games.AsQueryable();

            // filter
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.GameName.Contains(request.Keyword));
            }

            if (request.GenreID != null)
            {
                query = query.Where(x => x.GameInGenres.Any(x => x.GenreID == request.GenreID));
            }
            //paging

            int totalrow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).Select(x => new GameViewModel()
                {
                    CreatedDate = x.CreatedDate,
                    GameID = x.GameID,
                    Name = x.GameName,
                    Description = x.Description,
                    UpdatedDate = x.UpdatedDate,
                    Gameplay = x.Gameplay,
                    Discount = x.Discount,
                    GenreName = new List<string>(),
                    GenreIDs = x.GameInGenres.Select(y => y.GenreID).ToList(),
                    Status = x.Status.ToString(),
                    Price = x.Price,
                    ListImage = new List<string>(),
                    SRM = new SystemRequireMin()
                    {
                        OS = x.SystemRequirementMin.OS,
                        Processor = x.SystemRequirementMin.Processor,
                        Memory = x.SystemRequirementMin.Memory,
                        Graphics = x.SystemRequirementMin.Graphics,
                        Storage = x.SystemRequirementMin.Storage,
                        AdditionalNotes = x.SystemRequirementMin.Storage,
                        Soundcard = x.SystemRequirementMin.Soundcard
                    },

                    SRR = new SystemRequirementRecommend()
                    {
                        OS = x.SystemRequirementRecommended.OS,
                        Processor = x.SystemRequirementRecommended.Processor,
                        Memory = x.SystemRequirementRecommended.Memory,
                        Graphics = x.SystemRequirementRecommended.Graphics,
                        Storage = x.SystemRequirementRecommended.Storage,
                        AdditionalNotes = x.SystemRequirementRecommended.Storage,
                        Soundcard = x.SystemRequirementRecommended.Soundcard
                    }
                })
                .ToListAsync();
            var genres = _context.Genres.AsQueryable();
            foreach (var item in data)
            {
                foreach (var genre in item.GenreIDs)
                {
                    var name = genres.Where(x => x.GenreID == genre).Select(y => y.GenreName).FirstOrDefault();
                    item.GenreName.Add(name);
                }
            }
            var thumbnailimage = _context.GameImages.AsQueryable();
            foreach (var item in data)
            {
                var listgame = thumbnailimage.Where(x => x.GameID == item.GameID).Select(y => y.ImagePath).ToList();
                item.ListImage = listgame;
            }
            //select and projection
            var pagedResult = new PagedResult<GameViewModel>()
            {
                TotalRecords = totalrow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return pagedResult;
        }

        public async Task<int> Update(int GameID, GameEditRequest request)
        {
            var game = await _context.Games.Where(x => x.GameID == GameID)
                .Include(y => y.SystemRequirementMin)
                .Include(g => g.SystemRequirementRecommended)
                .FirstOrDefaultAsync();
            if (game == null)
            {
                throw new GameShopException($"Can not find a game");
            }
            else
            {
                game.GameName = request.Name;

                game.Discount = request.Discount;
                game.Description = request.Description;
                game.Gameplay = request.Gameplay;
                game.UpdatedDate = DateTime.Now;
                game.Status = (Status)request.Status;
                if (request.SRR != null)
                {
                    game.SystemRequirementRecommended.OS = request.SRR.OS;
                    game.SystemRequirementRecommended.Memory = request.SRR.Memory;
                    game.SystemRequirementRecommended.Processor = request.SRR.Processor;
                    game.SystemRequirementRecommended.Graphics = request.SRR.Graphics;
                    game.SystemRequirementRecommended.Storage = request.SRR.Storage;
                    game.SystemRequirementRecommended.AdditionalNotes = request.SRR.AdditionalNotes;
                    game.SystemRequirementRecommended.Soundcard = request.SRR.Soundcard;
                }
                if (request.SRM != null)
                {
                    game.SystemRequirementMin.OS = request.SRM.OS;
                    game.SystemRequirementMin.Memory = request.SRM.Memory;
                    game.SystemRequirementMin.Processor = request.SRM.Processor;
                    game.SystemRequirementMin.Graphics = request.SRM.Graphics;
                    game.SystemRequirementMin.Storage = request.SRM.Storage;
                    game.SystemRequirementMin.AdditionalNotes = request.SRM.AdditionalNotes;
                    game.SystemRequirementMin.Soundcard = request.SRM.Soundcard;
                }
                if (request.ThumbnailImage != null)
                {
                    var thumbnailImage = await _context.GameImages
                        .FirstOrDefaultAsync(i => i.isDefault == true && i.GameID == request.GameID);
                    if (thumbnailImage != null)
                    {
                        thumbnailImage.Filesize = request.ThumbnailImage.Length;
                        thumbnailImage.ImagePath = await this.Savefile(request.ThumbnailImage);
                        _context.GameImages.Update(thumbnailImage);
                    }
                    else
                    {
                        game.GameImages = new List<GameImage>()       {
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
                }
                _context.Games.Update(game);
                return await _context.SaveChangesAsync();
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

        public async Task<GameViewModel> GetById(int GameID)
        {
            var categories = await (from c in _context.Genres
                                    join pic in _context.GameinGenres on c.GenreID equals pic.GenreID
                                    where pic.GameID == GameID
                                    select c.GenreName).ToListAsync();
            var gameview = await _context.Games.Where(x => x.GameID == GameID).Select(x => new GameViewModel()
            {
                GameID = x.GameID,
                Name = x.GameName,
                Gameplay = x.Gameplay,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                GenreIDs = new List<int>(),
                GenreName = categories,
                Description = x.Description,
                Discount = x.Discount,
                Price = x.Price,
                ListImage = new List<string>(),
                SRM = new SystemRequireMin()
                {
                    OS = x.SystemRequirementMin.OS,
                    Processor = x.SystemRequirementMin.Processor,
                    Memory = x.SystemRequirementMin.Memory,
                    Graphics = x.SystemRequirementMin.Graphics,
                    Storage = x.SystemRequirementMin.Storage,
                    AdditionalNotes = x.SystemRequirementMin.Storage,
                    Soundcard = x.SystemRequirementMin.Soundcard
                },

                SRR = new SystemRequirementRecommend()
                {
                    OS = x.SystemRequirementRecommended.OS,
                    Processor = x.SystemRequirementRecommended.Processor,
                    Memory = x.SystemRequirementRecommended.Memory,
                    Graphics = x.SystemRequirementRecommended.Graphics,
                    Storage = x.SystemRequirementRecommended.Storage,
                    AdditionalNotes = x.SystemRequirementRecommended.Storage,
                    Soundcard = x.SystemRequirementRecommended.Soundcard
                }
            }).FirstOrDefaultAsync();

            var genres = await _context.GameinGenres.Where(x => x.GameID == gameview.GameID).ToListAsync();

            foreach (var genre in genres)
            {
                gameview.GenreIDs.Add(genre.GenreID);
            }
            var thumbnailimage = _context.GameImages.AsQueryable();

            var listgame = thumbnailimage.Where(x => x.GameID == gameview.GameID).Select(y => y.ImagePath).ToList();
            gameview.ListImage = listgame;

            return gameview;
        }

        public async Task<int> AddImage(int GameID, GameImageCreateRequest newimage)
        {
            var gameimage = new GameImage()
            {
                Caption = newimage.Caption,
                CreatedDate = DateTime.Now,
                isDefault = newimage.isDefault,
                GameID = GameID,
                SortOrder = newimage.SortOrder
            };
            if (newimage.ImageFile != null)
            {
                gameimage.ImagePath = await this.Savefile(newimage.ImageFile);
                gameimage.Filesize = newimage.ImageFile.Length;
            }
            _context.GameImages.Add(gameimage);
            await _context.SaveChangesAsync();
            return gameimage.ImageID;
        }

        public async Task<int> RemoveImage(int ImageID)
        {
            var gameimage = await _context.GameImages.FindAsync(ImageID);
            if (gameimage != null)
            {
                _context.GameImages.Remove(gameimage);
                return await _context.SaveChangesAsync();
            }
            else
            {
                throw new GameShopException($"Could not find an image with id{ImageID}");
            }
        }

        public async Task<int> UpdateImage(int ImageID, GameImageUpdateRequest Image)
        {
            var gameimage = await _context.GameImages.FindAsync(ImageID);
            if (gameimage != null)
            {
                gameimage.SortOrder = Image.SortOrder;
                gameimage.isDefault = Image.isDefault;
                gameimage.Caption = Image.Caption;
                if (Image.ImageFile != null)
                {
                    gameimage.ImagePath = await this.Savefile(Image.ImageFile);
                    gameimage.Filesize = Image.ImageFile.Length;
                }
            }
            else
            {
                throw new GameShopException($"Could not find an image with id{ImageID}");
            }
            _context.GameImages.Update(gameimage);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<GameImageViewModel>> GetListImages(int GameID)
        {
            return await _context.GameImages.Where(x => x.GameID == GameID)
                .Select(i => new GameImageViewModel()
                {
                    FilePath = i.ImagePath,
                    Caption = i.Caption,
                    CreatedDate = i.CreatedDate,
                    FileSize = i.Filesize,
                    ImageID = i.ImageID,
                    isDefault = i.isDefault,
                    GameID = GameID,
                    SortOrder = i.SortOrder,
                }).ToListAsync();
        }

        public async Task<GameImageViewModel> GetImageById(int ImageID)
        {
            var image = await _context.GameImages.FindAsync(ImageID);
            if (image == null)
            {
                throw new GameShopException($"Could not find this image");
            }
            var imageview = new GameImageViewModel()
            {
                FilePath = image.ImagePath,
                Caption = image.Caption,
                CreatedDate = image.CreatedDate,
                FileSize = image.Filesize,
                ImageID = image.ImageID,
                isDefault = image.isDefault,
                GameID = image.GameID,
                SortOrder = image.SortOrder,
            };
            return imageview;
        }

        public async Task<List<GameViewModel>> GetAll()
        {
            var data = await _context.Games.Select(x => new GameViewModel()
            {
                CreatedDate = DateTime.Now,
                GameID = x.GameID,
                Name = x.GameName,
                Description = x.Description,
                UpdatedDate = x.UpdatedDate,
                Gameplay = x.Gameplay,
                Discount = x.Discount,
                GenreName = new List<string>(),
                GenreIDs = x.GameInGenres.Select(y => y.GenreID).ToList(),
                Status = x.Status.ToString(),
                SRM = new SystemRequireMin()
                {
                    OS = x.SystemRequirementMin.OS,
                    Processor = x.SystemRequirementMin.Processor,
                    Memory = x.SystemRequirementMin.Memory,
                    Graphics = x.SystemRequirementMin.Graphics,
                    Storage = x.SystemRequirementMin.Storage,
                    AdditionalNotes = x.SystemRequirementMin.Storage,
                    Soundcard = x.SystemRequirementMin.Soundcard
                },
                Price = x.Price,
                SRR = new SystemRequirementRecommend()
                {
                    OS = x.SystemRequirementRecommended.OS,
                    Processor = x.SystemRequirementRecommended.Processor,
                    Memory = x.SystemRequirementRecommended.Memory,
                    Graphics = x.SystemRequirementRecommended.Graphics,
                    Storage = x.SystemRequirementRecommended.Storage,
                    AdditionalNotes = x.SystemRequirementRecommended.Storage,
                    Soundcard = x.SystemRequirementRecommended.Soundcard
                }
            }).ToListAsync();

            var genres = _context.Genres.AsQueryable();
            foreach (var item in data)
            {
                foreach (var genre in item.GenreIDs)
                {
                    var name = genres.Where(x => x.GenreID == genre).Select(y => y.GenreName).FirstOrDefault();
                    item.GenreName.Add(name);
                }
            }
            var thumbnailimage = _context.GameImages.AsQueryable();
            foreach (var item in data)
            {
                var listgame = thumbnailimage.Where(x => x.GameID == item.GameID).Select(y => y.ImagePath).ToList();
                item.ListImage = listgame;
            }
            var newdata = data.OrderBy(x => x.UpdatedDate).Take(5).ToList();
            return newdata;
        }

        //public async Task<PagedResult<GameViewModel>> GetAllbyGenreID(GetPublicGamePagingRequest request)
        //{
        //    var query = _context.Games.AsQueryable();
        //    // filter
        //    if (request.GenreID.HasValue && request.GenreID.Value > 0)
        //    {
        //        query = query.Where(x => x.GameInGenres.Any(x => x.GenreID == request.GenreID));
        //    }

        //    //paging
        //    int totalrow = await query.CountAsync();
        //    var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
        //        .Take(request.PageSize)
        //        .Select(x => new GameViewModel()
        //        {
        //            GameID = x.GameID,
        //            Name = x.GameName,
        //            Gameplay = x.Gameplay,
        //            Price = x.Price,
        //            Discount = x.Discount,
        //            GenreIDs = x.GameInGenres.Select(y => y.GenreID).ToList(),
        //            Description = x.Description,
        //            CreatedDate = x.CreatedDate,
        //            UpdatedDate = x.UpdatedDate,
        //            SRM = new SystemRequireMin()
        //            {
        //                OS = x.SystemRequirementMin.OS,
        //                Processor = x.SystemRequirementMin.Processor,
        //                Memory = x.SystemRequirementMin.Memory,
        //                Graphics = x.SystemRequirementMin.Graphics,
        //                Storage = x.SystemRequirementMin.Storage,
        //                AdditionalNotes = x.SystemRequirementMin.Storage,
        //                Soundcard = x.SystemRequirementMin.Soundcard
        //            },
        //            SRR = new SystemRequirementRecommend()
        //            {
        //                OS = x.SystemRequirementRecommended.OS,
        //                Processor = x.SystemRequirementRecommended.Processor,
        //                Memory = x.SystemRequirementRecommended.Memory,
        //                Graphics = x.SystemRequirementRecommended.Graphics,
        //                Storage = x.SystemRequirementRecommended.Storage,
        //                AdditionalNotes = x.SystemRequirementRecommended.Storage,
        //                Soundcard = x.SystemRequirementRecommended.Soundcard
        //            }
        //        }).ToListAsync();
        //    var genres = _context.Genres.AsQueryable();
        //    foreach (var item in data)
        //    {
        //        foreach (var genre in item.GenreIDs)
        //        {
        //            var name = genres.Where(x => x.GenreID == genre).Select(y => y.GenreName).FirstOrDefault();
        //            item.GenreName.Add(name);
        //        }
        //    };
        //    var thumbnailimage = _context.GameImages.AsQueryable();
        //    foreach (var item in data)
        //    {
        //        var listgame = thumbnailimage.Where(x => x.GameID == item.GameID).Select(y => y.ImagePath).ToList();
        //        item.ListImage = listgame;
        //    }
        //    //select and projection
        //    var pagedResult = new PagedResult<GameViewModel>()
        //    {
        //        TotalRecords = totalrow,
        //        PageSize = request.PageSize,
        //        PageIndex = request.PageIndex,
        //        Items = data
        //    };
        //    return pagedResult;
        //}

        public async Task<string> Savefile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var filename = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), filename);
            return filename;
        }

        public async Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return new ApiErrorResult<bool>($"Sản phẩm với id {id} không tồn tại");
            }
            foreach (var genre in request.Categories)
            {
                var gameInGenre = await _context.GameinGenres
                    .FirstOrDefaultAsync(x => x.GenreID == int.Parse(genre.Id)
                    && x.GameID == id);
                if (gameInGenre != null && genre.Selected == false)
                {
                    _context.GameinGenres.Remove(gameInGenre);
                }
                else if (gameInGenre == null && genre.Selected)
                {
                    await _context.GameinGenres.AddAsync(new GameinGenre()
                    {
                        GenreID = int.Parse(genre.Id),
                        GameID = id
                    });
                }
            }
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }
    }
}