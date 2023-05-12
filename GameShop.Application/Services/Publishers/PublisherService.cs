using AutoMapper;
using FRT.DataReporting.Application.Utilities;
using FRT.DataReporting.Domain.Configurations;
using GameShop.Data.EF;
using GameShop.Data.Entities;
using GameShop.Utilities.Exceptions;
using GameShop.ViewModels.Catalog.Publishers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Rijndael256;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
           if(await _context.Publishers.Where(x=>x.Name.ToLower() == req.Name.ToLower().Trim()).FirstOrDefaultAsync() != null)
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
            var publisher = await _context.Publishers.Where(x=>x.Id == publisherId).Include(x=>x.Games).FirstOrDefaultAsync();
            if(publisher == null)
            {
                throw new GameShopException("không tìm thấy publisher");
            }
            var keyList = new List<string>();
            string password = "sKzvYk#1Pn33!YN";  
            foreach( var game in publisher.Games)
            {   
                for(var i = 0 ; i < amount; i++)
                {
                    string plaintext = game.GameName + publisher.Name + game.Id + DateTime.Now;
                    string ciphertext = Rijndael.Encrypt(plaintext, password, KeySize.Aes256);
                    var newKey = new Key()
                    {
                        KeyCode = ciphertext,
                        PublisherName = publisher.Name,
                        GameName = game.GameName,
                        Status = true
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
                var keys = await _context.Keys.Where(x=>x.PublisherName== publisher.Name && x.GameName == game.GameName).ToListAsync();
            List<HashEntry> entries = new List<HashEntry>();
           foreach( var key in keys)
            {
                var hashKey = new HashEntry(key.Id.ToString(), JsonConvert.SerializeObject(key));
                entries.Add(hashKey);
            }
         
                await _redisUtil.SetMultiAsync(string.Format(_redisConfig.DSMKey, publisher.Name, game.GameName), entries.ToArray(), null);

            }



            return keyList;

        }

        public async Task<List<PublisherDTO>> GetAllPublisher()
        {
            var publisherList = await _context.Publishers.ToListAsync();
            return _mapper.Map<List<PublisherDTO>>(publisherList);
        }
    }
}
