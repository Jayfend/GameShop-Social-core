using AutoMapper;
using GameShop.Data.EF;
using GameShop.Data.Entities;
using GameShop.Utilities.Configurations;
using GameShop.Utilities.Exceptions;
using GameShop.Utilities.Redis;
using GameShop.ViewModels.Catalog.Publishers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Rijndael256;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Publisher = GameShop.Data.Entities.Publisher;

namespace GameShop.Application.Services.Publishers
{
    public class PublisherService : IPublisherService
    {
        private readonly GameShopDbContext _context;
        private readonly IMapper _mapper;
        readonly IRedisUtil _redisUtil;
        readonly RedisConfig _redisConfig;
        public PublisherService(GameShopDbContext context, IMapper mapper, IRedisUtil redisUtil, IOptions<RedisConfig> redisConfig)
        {
            _context = context;
            _mapper = mapper;
            _redisUtil = redisUtil;
            _redisConfig = redisConfig.Value;
        }
        public async Task<PublisherDTO> CreateAsync(PublisherCreateDTO req)
        {
            if (await _context.Publishers.Where(x => x.Name.ToLower() == req.Name.ToLower().Trim()).FirstOrDefaultAsync() != null)
            {
                throw new GameShopException("Tên đã bị trùng");
            }

            var newPublisher = new Publisher()
            {
                Name = req.Name,
                CreatedDate = DateTime.Now,
            };
            await _context.Publishers.AddAsync(newPublisher);
            await _context.SaveChangesAsync();
            return _mapper.Map<PublisherDTO>(newPublisher);
        }

        public async Task<List<string>> GenerateKeyAsync(Guid publisherId, int amount)
        {
            var publisher = await _context.Publishers.Where(x => x.Id == publisherId).Include(x => x.Games).FirstOrDefaultAsync();
            if (publisher == null)
            {
                throw new GameShopException("không tìm thấy publisher");
            }
            var keyList = new List<string>();
            string password = "sKzvYk#1Pn33!YN";
            foreach (var game in publisher.Games)
            {
                for (var i = 0; i < amount; i++)
                {
                    string plaintext = game.GameName + publisher.Name + game.Id + DateTime.Now;
                    string ciphertext = Rijndael.Encrypt(plaintext, password, KeySize.Aes256);
                    var newKey = new Data.Entities.Key()
                    {
                        KeyCode = ciphertext,
                        PublisherName = publisher.Name,
                        GameName = game.GameName,
                        Status = true,
                        isActive = false
                    };
                    await _context.Keys.AddAsync(newKey);
                    keyList.Add(ciphertext);
                }

                //// Giải mã chuỗi
                //plaintext = Rijndael.Decrypt(ciphertext, password, KeySize.Aes256);
            }
            await _context.SaveChangesAsync();
            foreach (var game in publisher.Games)
            {
                var keys = await _context.Keys.Where(x => x.PublisherName == publisher.Name && x.GameName == game.GameName).ToListAsync();
                List<HashEntry> entries = new List<HashEntry>();
                foreach (var key in keys)
                {
                    var hashKey = new HashEntry(key.Id.ToString(), JsonConvert.SerializeObject(key));
                    entries.Add(hashKey);
                }
                if (entries.Any())
                {
                    await _redisUtil.SetMultiAsync(string.Format(_redisConfig.DSMKey, publisher.Name, game.GameName), entries.ToArray(), null);
                }

            }



            return keyList;

        }

        public async Task<List<PublisherDTO>> GetAllPublisher()
        {
            var publisherList = await _context.Publishers.ToListAsync();
            return _mapper.Map<List<PublisherDTO>>(publisherList);
        }

        public async Task<bool> SyncKey()
        {
            string password = "sKzvYk#1Pn33!YN";
            var publisherList = await _context.Publishers.ToListAsync();
            foreach (var publisher in publisherList)
            {
                var gameList = await _context.Games.Where(x => x.PublisherId == publisher.Id).ToListAsync();

                foreach (var game in gameList)
                {

                    var keys = await _redisUtil.HashGetAllAsync(string.Format(_redisConfig.DSMKey, publisher.Name, game.GameName));
                    foreach (var key in keys)
                    {
                        var parsedKey = JsonConvert.DeserializeObject<Data.Entities.Key>(key);
                        _context.Keys.Update(parsedKey);
                    }
                    List<HashEntry> entries = new List<HashEntry>();
                    for (var i = 0; i < 10; i++)
                    {
                        string plaintext = game.GameName + publisher.Name + game.Id + DateTime.Now;
                        string ciphertext = Rijndael.Encrypt(plaintext, password, KeySize.Aes256);
                        var newKey = new Data.Entities.Key()
                        {
                            KeyCode = ciphertext,
                            PublisherName = publisher.Name,
                            GameName = game.GameName,
                            Status = true,
                            isActive = false
                        };
                        await _context.Keys.AddAsync(newKey);
                        var hashKey = new HashEntry(newKey.Id.ToString(), JsonConvert.SerializeObject(newKey));
                        entries.Add(hashKey);
                    }
                    if (entries.Any())
                    {
                        await _redisUtil.SetMultiAsync(string.Format(_redisConfig.DSMKey, publisher.Name, game.GameName), entries.ToArray(), null);
                    }

                }


            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
