﻿using AutoMapper;
using GameShop.Application.Common;
using GameShop.Data.EF;
using GameShop.Data.Entities;
using GameShop.Utilities;
using GameShop.Utilities.Configurations;
using GameShop.Utilities.Exceptions;
using GameShop.Utilities.Redis;
using GameShop.ViewModels.Catalog.GameImages;
using GameShop.ViewModels.Catalog.Games;
using GameShop.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Nest;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GameShop.Application.Services.Games
{
    public class GameService : IGameService
    {
        private readonly GameShopDbContext _context;
        private readonly IStorageService _storageService;
        private readonly UserManager<AppUser> _userManager;
        readonly IRedisUtil _redisUtil;
        readonly RedisConfig _redisConfig;
        readonly IElasticSearchUlti _elasticSearchUtil;
        readonly ElasticSearchConfig _elasticSearchConfig;
        readonly IMapper _mapper;
        public GameService(GameShopDbContext context
            , IStorageService storageService
            , IRedisUtil redisUtil
            , IOptions<RedisConfig> redisConfig
            , UserManager<AppUser> useManager
            , IElasticSearchUlti elasticSearchUtil
            , IOptions<ElasticSearchConfig> elasticSearchConfig
            , IMapper mapper)
        {
            _context = context;
            _storageService = storageService;
            _redisUtil = redisUtil;
            _redisConfig = redisConfig.Value;
            _userManager = useManager;
            _elasticSearchConfig = elasticSearchConfig.Value;
            _elasticSearchUtil = elasticSearchUtil;
            _mapper = mapper;

        }

        public async Task<Guid> Create(GameCreateRequest request)
        {
            if (await _context.Publishers.Where(x => x.Id == request.PublisherId).FirstOrDefaultAsync() == null)
            {
                throw new GameShopException("Không tìm thấy nhà phát hành");
            }
            var game = new Game()
            {
                GameName = request.GameName,
                Price = request.Price,
                Description = request.Description,
                Discount = request.Discount,
                Gameplay = request.Gameplay,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                PublisherId = request.PublisherId,
                RatePoint = 0,
                Status = request.Status
            };
            var genrelist = await _context.Genres.Where(x => request.GenreList.Contains(x.Id)).ToListAsync();

            var newGameInGenreList = new List<GameinGenre>();
            foreach(var genre in genrelist)
            {
                var newgameingenre = new GameinGenre()
                {
                    Game = game,
                    Genre = genre
                };
                newGameInGenreList.Add(newgameingenre);
            }

             _context.GameinGenres.AddRange(newGameInGenreList);

            if (request.ThumbnailImage != null)
            {
                game.GameImages = new List<GameImage>()
                {
                     new GameImage()
                     {
                         Caption="Thumbnail Image",
                         CreatedDate = DateTime.Now,
                         Filesize = request.ThumbnailImage.Length,
                         ImagePath=  await Savefile(request.ThumbnailImage),
                         isDefault = true,
                         SortOrder = 1,
                     }
                };
            }
            if (request.FileGame != null)
            {
                game.FilePath = await Savefile(request.FileGame);
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
            var gameView =new GameViewModel()
            {
                CreatedDate = game.CreatedDate,
                Id = game.Id,
                Name = game.GameName,
                Description = game.Description,
                UpdatedDate = game.UpdatedDate,
                Gameplay = game.Gameplay,
                Discount = game.Discount,
                PublisherId = game.PublisherId,
                PublisherName = game.Publisher.Name,
                GenreName = new List<string>(),
                GenreIDs = game.GameInGenres.Select(y => y.GenreID).ToList(),
                Status = game.Status,
                Price = game.Price,
                ListImage = new List<string>(),
                SRM = new SystemRequireMin()
                {
                    OS = game.SystemRequirementMin.OS,
                    Processor = game.SystemRequirementMin.Processor,
                    Memory = game.SystemRequirementMin.Memory,
                    Graphics = game.SystemRequirementMin.Graphics,
                    Storage = game.SystemRequirementMin.Storage,
                    AdditionalNotes = game.SystemRequirementMin.Storage,
                    Soundcard = game.SystemRequirementMin.Soundcard
                },

                SRR = new SystemRequirementRecommend()
                {
                    OS = game.SystemRequirementRecommended.OS,
                    Processor = game.SystemRequirementRecommended.Processor,
                    Memory = game.SystemRequirementRecommended.Memory,
                    Graphics = game.SystemRequirementRecommended.Graphics,
                    Storage = game.SystemRequirementRecommended.Storage,
                    AdditionalNotes = game.SystemRequirementRecommended.Storage,
                    Soundcard = game.SystemRequirementRecommended.Soundcard
                }
            };
            var genres = _context.Genres.AsQueryable();
        
                foreach (var genre in gameView.GenreIDs)
                {
                    var name = genres.Where(x => x.Id == genre).Select(y => y.GenreName).FirstOrDefault();
                gameView.GenreName.Add(name);
                }
            
            var thumbnailimage = _context.GameImages.AsQueryable();
           
                var listgame = thumbnailimage.Where(x => x.GameID == gameView.Id).Select(y => y.ImagePath).ToList();
            gameView.ListImage = listgame;
            
            var elasticGame = _mapper.Map<GameElasticModel>(gameView);
            elasticGame.GenreSuggest = new CompletionField
            {
                Input = elasticGame.GenreName.ToArray()
            };

            await _elasticSearchUtil.AddAsync(elasticGame, _elasticSearchConfig.Common.GameIndex, ElasticServer.Common);


            return game.Id;
        }

        public async Task<int> Delete(Guid GameID)
        {
            var game = await _context.Games.FindAsync(GameID);
            if (game == null)
            {
                throw new GameShopException($"Can not find a game");
            }
            else
            {
                //var thumbnailImages = _context.GameImages.Where(i => i.GameID == GameID);
                //foreach (var item in thumbnailImages)
                //{
                //    await _storageService.DeleteFileAsync(item.ImagePath);
                //    _context.GameImages.Remove(item);
                //}
                game.IsDelete = true;
                _context.Games.Update(game);
                var elasticGame = _mapper.Map<GameElasticModel>(game);

                 await _elasticSearchUtil.DeleteAsync<GameElasticModel>(elasticGame.ESId, _elasticSearchConfig.Common.GameIndex, ElasticServer.Common);
                return await _context.SaveChangesAsync();
            }
        }

        public async Task<PagedResult<GameViewModel>> GetAllPaging(GetManageGamePagingRequest request)
        {
            var query = _context.Games.Where(x=>x.IsDelete == false).AsQueryable();

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
                    Id = x.Id,
                    Name = x.GameName,
                    Description = x.Description,
                    UpdatedDate = x.UpdatedDate,
                    Gameplay = x.Gameplay,
                    Discount = x.Discount,
                    PublisherId = x.PublisherId,
                    PublisherName = x.Publisher.Name,
                    GenreName = new List<string>(),
                    GenreIDs = x.GameInGenres.Select(y => y.GenreID).ToList(),
                    Status = x.Status,
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
                    var name = genres.Where(x => x.Id == genre).Select(y => y.GenreName).FirstOrDefault();
                    item.GenreName.Add(name);
                }
            }
            var thumbnailimage = _context.GameImages.AsQueryable();
            foreach (var item in data)
            {
                var listgame = thumbnailimage.Where(x => x.GameID == item.Id).Select(y => y.ImagePath).ToList();
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
            //elastic
            //var suggestions = _elasticSearchUtil.SearchSuggestion(request.Keyword, _elasticSearchConfig.Common.GameIndex, ElasticServer.Common);
            return pagedResult;
        }

        public async Task<int> Update(Guid GameID, GameEditRequest request)
        {
            var game = await _context.Games.Where(x => x.Id == GameID)
                .Include(y => y.SystemRequirementMin)
                .Include(g => g.SystemRequirementRecommended)
                .Include(x=>x.GameInGenres)
                .Include(x=>x.GameImages)
                .Include(x=>x.Publisher)
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
                game.Status = request.Status;
                game.PublisherId = request.PublisherId;
                game.Price = request.Price;
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
                        .FirstOrDefaultAsync(i => i.isDefault == true && i.GameID == request.Id);
                    if (thumbnailImage != null)
                    {
                        thumbnailImage.Filesize = request.ThumbnailImage.Length;
                        thumbnailImage.ImagePath = await Savefile(request.ThumbnailImage);
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
                         ImagePath=  await Savefile(request.ThumbnailImage),
                         isDefault = true,
                         SortOrder = 1,
                          }
                        };
                    }
                }
                if (request.FileGame != null)
                {
                    game.FilePath = await Savefile(request.FileGame);
                }
                _context.Games.Update(game);
                var gameView = new GameViewModel()
                {
                    CreatedDate = game.CreatedDate,
                    Id = game.Id,
                    Name = game.GameName,
                    Description = game.Description,
                    UpdatedDate = game.UpdatedDate,
                    Gameplay = game.Gameplay,
                    Discount = game.Discount,
                    PublisherId = game.PublisherId,
                    PublisherName = game.Publisher.Name,
                    GenreName = new List<string>(),
                    GenreIDs = game.GameInGenres.Select(y => y.GenreID).ToList(),
                    Status = game.Status,
                    Price = game.Price,
                    ListImage = new List<string>(),
                    SRM = new SystemRequireMin()
                    {
                        OS = game.SystemRequirementMin.OS,
                        Processor = game.SystemRequirementMin.Processor,
                        Memory = game.SystemRequirementMin.Memory,
                        Graphics = game.SystemRequirementMin.Graphics,
                        Storage = game.SystemRequirementMin.Storage,
                        AdditionalNotes = game.SystemRequirementMin.Storage,
                        Soundcard = game.SystemRequirementMin.Soundcard
                    },

                    SRR = new SystemRequirementRecommend()
                    {
                        OS = game.SystemRequirementRecommended.OS,
                        Processor = game.SystemRequirementRecommended.Processor,
                        Memory = game.SystemRequirementRecommended.Memory,
                        Graphics = game.SystemRequirementRecommended.Graphics,
                        Storage = game.SystemRequirementRecommended.Storage,
                        AdditionalNotes = game.SystemRequirementRecommended.Storage,
                        Soundcard = game.SystemRequirementRecommended.Soundcard
                    }
                };
                var genres = _context.Genres.AsQueryable();

                foreach (var genre in gameView.GenreIDs)
                {
                    var name = genres.Where(x => x.Id == genre).Select(y => y.GenreName).FirstOrDefault();
                    gameView.GenreName.Add(name);
                }

                var thumbnailimage = _context.GameImages.AsQueryable();

                var listgame = thumbnailimage.Where(x => x.GameID == gameView.Id).Select(y => y.ImagePath).ToList();
                gameView.ListImage = listgame;

                var elasticGame = _mapper.Map<GameElasticModel>(gameView);
                elasticGame.GenreSuggest = new CompletionField
                {
                    Input = elasticGame.GenreName.ToArray()
                };

                await _elasticSearchUtil.UpdateAsync(elasticGame, _elasticSearchConfig.Common.GameIndex, ElasticServer.Common);

                return await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> UpdatePrice(Guid GameID, decimal newPrice)
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

        public async Task<GameViewModelWithSuggestion> GetById(Guid GameID)
        {
            var categories = await (from c in _context.Genres
                                    join pic in _context.GameinGenres on c.Id equals pic.GenreID
                                    where pic.GameId == GameID
                                    select c.GenreName).ToListAsync();
            var gameview = await _context.Games.Where(x => x.Id == GameID).Include(x=>x.GameInGenres).Include(x=>x.GameImages).Select(x => new GameViewModelWithSuggestion()
            {
                Id = x.Id,
                Name = x.GameName,
                Gameplay = x.Gameplay,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                GenreIDs = x.GameInGenres.Select(x=>x.GenreID).ToList(),
                Status = x.Status,
                GenreName = categories,
                Description = x.Description,
                Discount = x.Discount,
                Price = x.Price,
                PublisherId = x.PublisherId,
                PublisherName = x.Publisher.Name,
                ListImage = x.GameImages.Select(x=>x.ImagePath).ToList(),
                IsDelete = x.IsDelete,
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
            var ratings = await _context.Ratings.Where(x => x.GameId == GameID).Select(x => x.Point).ToListAsync();
            if (ratings.Any())
            {
                int pointRating = 0;
                foreach (var point in ratings)
                {
                    pointRating = pointRating + point;
                }
                gameview.RatePoint = pointRating / (ratings.Count());

            }
            else
            {
                gameview.RatePoint = 0;
            }

            //var genres = await _context.GameinGenres.Where(x => x.GameId == gameview.Id).ToListAsync();

            //foreach (var genre in genres)
            //{
            //    gameview.GenreIDs.Add(genre.GenreID);
            //}
            //var thumbnailimage = await _context.GameImages.ToListAsync();

            //var listGameImage = thumbnailimage.Where(x => x.GameID == gameview.Id).Select(y => y.ImagePath).ToList();
            //gameview.ListImage = listGameImage;


            //elastic
            var suggestions = await _elasticSearchUtil.SearchSuggestion(gameview.GenreName, _elasticSearchConfig.Common.GameIndex, ElasticServer.Common);
            var deleteItemList = suggestions.Where(x=>x.Id == gameview.Id).ToList();
            foreach(var deleteItem in deleteItemList)
            {
                suggestions.Remove(deleteItem);
            }
           
            gameview.GameSuggestionList = suggestions.Distinct().ToList();
            return gameview;
        }

        public async Task<Guid> AddImage(Guid GameID, GameImageCreateRequest newimage)
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
                gameimage.ImagePath = await Savefile(newimage.ImageFile);
                gameimage.Filesize = newimage.ImageFile.Length;
            }
            _context.GameImages.Add(gameimage);
            await _context.SaveChangesAsync();
            return gameimage.Id;
        }

        public async Task<int> RemoveImage(Guid ImageID)
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

        public async Task<int> UpdateImage(Guid ImageID, GameImageUpdateRequest Image)
        {
            var gameimage = await _context.GameImages.FindAsync(ImageID);
            if (gameimage != null)
            {
                gameimage.SortOrder = Image.SortOrder;
                gameimage.isDefault = Image.isDefault;
                gameimage.Caption = Image.Caption;
                if (Image.ImageFile != null)
                {
                    gameimage.ImagePath = await Savefile(Image.ImageFile);
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

        public async Task<List<GameImageViewModel>> GetListImages(Guid GameID)
        {
            return await _context.GameImages.Where(x => x.GameID == GameID)
                .Select(i => new GameImageViewModel()
                {
                    FilePath = i.ImagePath,
                    Caption = i.Caption,
                    CreatedDate = i.CreatedDate,
                    FileSize = i.Filesize,
                    ImageID = i.Id,
                    isDefault = i.isDefault,
                    GameID = GameID,
                    SortOrder = i.SortOrder,
                }).ToListAsync();
        }

        public async Task<GameImageViewModel> GetImageById(Guid ImageID)
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
                ImageID = image.Id,
                isDefault = image.isDefault,
                GameID = image.GameID,
                SortOrder = image.SortOrder,
            };
            return imageview;
        }

        public async Task<PagedResult<GameViewModel>> GetAll(GetManageGamePagingRequest request)
        {
            var query = _context.Games.Where(x=>x.IsDelete == false).AsQueryable();

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
            var data = await query.Select(x => new GameViewModel()
            {
                CreatedDate = x.CreatedDate,
                Id = x.Id,
                Name = x.GameName,
                Description = x.Description,
                UpdatedDate = x.UpdatedDate,
                Gameplay = x.Gameplay,
                Discount = x.Discount,
                GenreName = new List<string>(),
                GenreIDs = x.GameInGenres.Select(y => y.GenreID).ToList(),
                Status = x.Status,
                Price = x.Price,
                PublisherId = x.PublisherId,
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
                    var name = genres.Where(x => x.Id == genre).Select(y => y.GenreName).FirstOrDefault();
                    item.GenreName.Add(name);
                }
            }
            var thumbnailimage = _context.GameImages.AsQueryable();
            foreach (var item in data)
            {
                var listgame = thumbnailimage.Where(x => x.GameID == item.Id).Select(y => y.ImagePath).ToList();
                item.ListImage = listgame;
            }
            var newdata = data.OrderByDescending(x => x.CreatedDate).ToList();
            newdata = newdata.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).ToList();
            //select and projection
            var pagedResult = new PagedResult<GameViewModel>()
            {
                TotalRecords = totalrow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = newdata
            };
            return pagedResult;
        }

        public async Task<PagedResult<GameViewModel>> GetSaleGames(GetManageGamePagingRequest request)
        {
            var query = _context.Games.Where(x => x.Discount > 0 && x.IsDelete == false);

            // filter

            //paging

            int totalrow = await query.CountAsync();
            var data = await query.Select(x => new GameViewModel()
            {
                CreatedDate = x.CreatedDate,
                Id = x.Id,
                Name = x.GameName,
                Description = x.Description,
                UpdatedDate = x.UpdatedDate,
                Gameplay = x.Gameplay,
                Discount = x.Discount,
                GenreName = new List<string>(),
                GenreIDs = x.GameInGenres.Select(y => y.GenreID).ToList(),
                Status = x.Status,
                Price = x.Price,
                PublisherId = x.PublisherId,
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
                    var name = genres.Where(x => x.Id == genre).Select(y => y.GenreName).FirstOrDefault();
                    item.GenreName.Add(name);
                }
            }
            var thumbnailimage = _context.GameImages.AsQueryable();
            foreach (var item in data)
            {
                var listgame = thumbnailimage.Where(x => x.GameID == item.Id).Select(y => y.ImagePath).ToList();
                item.ListImage = listgame;
            }
            var newdata = data.OrderByDescending(x => x.Discount).ToList();
            newdata = newdata.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).ToList();
            //select and projection
            var pagedResult = new PagedResult<GameViewModel>()
            {
                TotalRecords = totalrow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = newdata
            };
            return pagedResult;
        }

        public async Task<string> Savefile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var filename = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), filename);
            return filename;
        }

        public async Task<ApiResult<bool>> CategoryAssign(Guid id, CategoryAssignRequest request)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return new ApiErrorResult<bool>($"Sản phẩm với id {id} không tồn tại");
            }
            foreach (var genre in request.Categories)
            {
                var gameInGenre = await _context.GameinGenres
                    .FirstOrDefaultAsync(x => x.GenreID == genre.Id
                    && x.GameId == id);
                if (gameInGenre != null && genre.Selected == false)
                {
                    _context.GameinGenres.Remove(gameInGenre);
                }
                else if (gameInGenre == null && genre.Selected)
                {
                    await _context.GameinGenres.AddAsync(new GameinGenre()
                    {
                        GenreID = genre.Id,
                        GameId = id
                    });
                }
            }
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<PagedResult<GameBestSeller>> GetBestSeller(GetManageGamePagingRequest request)
        {
            var games = await _context.Games.Where(x=>x.IsDelete == false).Select(x => new GameBestSeller()
            {
                CreatedDate = x.CreatedDate,
                GameID = x.Id,
                Name = x.GameName,
                Description = x.Description,
                UpdatedDate = x.UpdatedDate,
                Gameplay = x.Gameplay,
                Discount = x.Discount,
                GenreName = new List<string>(),
                GenreIDs = x.GameInGenres.Select(y => y.GenreID).ToList(),
                Status = x.Status.ToString(),
                Price = x.Price,
                BuyCount = 0,
                PublisherId = x.PublisherId,
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

            var cartids = await _context.Checkouts.Select(x => x.CartID).ToListAsync();
            var orderedgames = await _context.OrderedGames.ToListAsync();
            foreach (var game in games)
            {
                foreach (var cartid in cartids)
                {
                    var check = orderedgames.Where(x => x.CartID == cartid && x.GameID == game.GameID).FirstOrDefault();
                    if (check != null)
                    {
                        game.BuyCount += 1;
                    }
                }
            }
            var genres = _context.Genres.AsQueryable();
            foreach (var item in games)
            {
                foreach (var genre in item.GenreIDs)
                {
                    var name = genres.Where(x => x.Id == genre).Select(y => y.GenreName).FirstOrDefault();
                    item.GenreName.Add(name);
                }
            }
            var thumbnailimage = _context.GameImages.AsQueryable();
            foreach (var item in games)
            {
                var listgame = thumbnailimage.Where(x => x.GameID == item.GameID).Select(y => y.ImagePath).ToList();
                item.ListImage = listgame;
            }
            int totalrow = games.Count();
            var data = games.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).OrderByDescending(x => x.BuyCount).ToList();
            var pagedResult = new PagedResult<GameBestSeller>()
            {
                TotalRecords = totalrow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return pagedResult;
        }

        public async Task<bool> ActiveGameAsync(ActiveGameDTO req)
        {
            if (string.IsNullOrWhiteSpace(req.Key))
            {
                throw new GameShopException("Vui lòng nhập Key");
            }
            var user = await _userManager.FindByIdAsync(req.UserId.ToString());
            if (user == null)
            {
                throw new GameShopException("Không tìm thấy user");
            }
            var game = await _context.Games.Where(x => x.Id == req.GameId).FirstOrDefaultAsync();
            if (game == null)
            {
                throw new GameShopException("Không tìm thấy game");
            }
            var checkoutList = await _context.Checkouts.Where(x => x.Cart.UserID == req.UserId).Select(x => x.Id).ToListAsync();
            var gameBought = await _context.SoldGames.Where(x => x.GameID == req.GameId && checkoutList.Contains(x.CheckoutID)).FirstOrDefaultAsync();
            if (gameBought == null)
            {
                throw new GameShopException("Bạn chưa mua game này");
            }
            var publisher = await _context.Publishers.Where(x => x.Id == game.PublisherId).FirstOrDefaultAsync();
            var keyList = await _redisUtil.HashGetAllAsync(string.Format(_redisConfig.DSMKey, publisher.Name, game.GameName));
            foreach (var _key in keyList)
            {
                var dbKey = JsonConvert.DeserializeObject<Data.Entities.Key>(_key);
                if (req.Key == dbKey.KeyCode)
                {
                    if (game.GameName != dbKey.GameName || publisher.Name != dbKey.PublisherName || dbKey.IsActive == true)
                    {
                        throw new GameShopException("Keycode không hợp lệ hoặc đã được sử dụng");
                    }
                    else
                    {
                        List<HashEntry> entries = new List<HashEntry>();
                        dbKey.IsActive = true;
                        var hashKey = new HashEntry(dbKey.Id.ToString(), JsonConvert.SerializeObject(dbKey));
                        entries.Add(hashKey);
                        await _redisUtil.SetMultiAsync(string.Format(_redisConfig.DSMKey, publisher.Name, gameBought.GameName), entries.ToArray(), null);
                        gameBought.isActive = true;
                        _context.SoldGames.Update(gameBought);
                        await _context.SaveChangesAsync();

                        return true;
                    }
                }
            }
           
            return false;
        }

        public async Task<bool> SyncElasticSearchGames()
        {
            var gameList = _context.Games.Where(x=>x.IsDelete == false).AsQueryable();
            var data = await gameList.Select(x => new GameViewModel()
            {
                CreatedDate = x.CreatedDate,
                Id = x.Id,
                Name = x.GameName,
                Description = x.Description,
                UpdatedDate = x.UpdatedDate,
                Gameplay = x.Gameplay,
                Discount = x.Discount,
                PublisherId = x.PublisherId,
                PublisherName = x.Publisher.Name,
                GenreName = new List<string>(),
                GenreIDs = x.GameInGenres.Select(y => y.GenreID).ToList(),
                Status = x.Status,
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
                    var name = genres.Where(x => x.Id == genre).Select(y => y.GenreName).FirstOrDefault();
                    item.GenreName.Add(name);
                }
            }
            var thumbnailimage = _context.GameImages.AsQueryable();
            foreach (var item in data)
            {
                var listgame = thumbnailimage.Where(x => x.GameID == item.Id).Select(y => y.ImagePath).ToList();
                item.ListImage = listgame;
            }
            foreach (var game in data)
            {
                var elasticGame = _mapper.Map<GameElasticModel>(game);
                elasticGame.GenreSuggest = new CompletionField
                {
                    Input = elasticGame.GenreName.ToArray()
                };

                await _elasticSearchUtil.AddAsync(elasticGame, _elasticSearchConfig.Common.GameIndex, ElasticServer.Common);
            }


            return true;
        }
    }
}