using FRT.DataReporting.Domain.Configurations;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FRT.DataReporting.Application.Utilities.Redis
{
    public class RedisUtil : IRedisUtil
    {
        readonly IDatabase _db;
        readonly IRedisConnectionFactory _authorizeRedisConnectionFactory;

        readonly RedisConfig _redisConfig;


        public RedisUtil(IRedisConnectionFactory authorizeRedisConnectionFactory
            , IOptions<RedisConfig> redisConfigOption)
        {
            _authorizeRedisConnectionFactory = authorizeRedisConnectionFactory;

            _redisConfig = redisConfigOption.Value;

            _db = _authorizeRedisConnectionFactory.GetDatabase(_redisConfig.Configuration);
        }

        public List<string> GetAllKeys(string searchText)
        {
            EndPoint endPoint = _db.Multiplexer.GetEndPoints().FirstOrDefault();

            List<RedisKey> keys;

            if (string.IsNullOrWhiteSpace(searchText))
            {
                keys = _db.Multiplexer.GetServer(endPoint).Keys().ToList();
            }
            else
            {
                keys = _db.Multiplexer.GetServer(endPoint).Keys(pattern: $"{searchText}").ToList();
            }


            return keys.Select(z => (string)z).ToList();
        }

        public async Task<T> GetAsync<T>(string key, string field)
        {
            T result = default;
            var isExist = string.IsNullOrWhiteSpace(field) ?
                            await _db.KeyExistsAsync(_redisConfig.Prefix + key)
                            : await _db.HashExistsAsync(_redisConfig.Prefix + key, field);
            if (isExist)
            {
                var value = string.IsNullOrWhiteSpace(field) ?
                    await _db.StringGetAsync(_redisConfig.Prefix + key)
                    : await _db.HashGetAsync(_redisConfig.Prefix + key, field);

                return JsonConvert.DeserializeObject<T>(value.ToString());

            }
            return result;
        }

        public async Task<T> GetOrAddAsync<T>(string key, string field, Func<Task<T>> func, TimeSpan? expireTime = null)
        {
            T result = await GetAsync<T>(key, field);

            if (result == null && func != null)
            {
                result = await func.Invoke();

                if (result != null)
                {
                    await SetAsync(key, field, JsonConvert.SerializeObject(result), expireTime);
                }
            }

            return result;
        }

        public async Task<bool> RemoveAsync(string key, string field, bool isHasPrefix = false)
        {
            string prefix = isHasPrefix ? "" : _redisConfig.Prefix;


            if (!string.IsNullOrWhiteSpace(field))
            {
                return await _db.HashDeleteAsync(prefix + key, field);
            }
            else
            {
                return await _db.KeyDeleteAsync(prefix + key);
            }
        }


        public async Task<bool> RemoveRangeKeyAsync(List<string> keys, List<string> fields = null, bool isHasPrefix = false)
        {

            if (fields?.Count > 0)
            {
                foreach (var key in keys)
                {
                    foreach (var field in fields)
                    {
                        await RemoveAsync(key, field, isHasPrefix);
                    }
                }
            }
            else
            {
                foreach (var key in keys)
                {
                    await RemoveAsync(key, null, isHasPrefix);

                }
            }


            return true;
        }

        public async Task<bool> SetAsync(string key, string field, string valueJson, TimeSpan? expireTime = null)
        {
            var result = string.IsNullOrWhiteSpace(field) ?
                await _db.StringSetAsync(_redisConfig.Prefix + key, valueJson)
                : await _db.HashSetAsync(_redisConfig.Prefix + key, field, valueJson);
            if (expireTime != null)
            {
                await _db.KeyExpireAsync(_redisConfig.Prefix + key, expireTime);
            }
            else
            {
                await _db.KeyExpireAsync(_redisConfig.Prefix + key, TimeSpan.FromSeconds(_redisConfig.AuthExpiresIn));
            }
            return result;

        }

        public async Task<bool> SetMultiAsync(string key, HashEntry[] field, string valueJson, TimeSpan? expireTime = null)
        {
            if (field.Any())
            {
                await _db.HashSetAsync(_redisConfig.Prefix + key, field);

            }
            else
            {
                await _db.StringSetAsync(_redisConfig.Prefix + key, valueJson);
            }
            if (expireTime != null)
            {
                await _db.KeyExpireAsync(_redisConfig.Prefix + key, expireTime);
            }
            else
            {
                await _db.KeyExpireAsync(_redisConfig.Prefix + key, TimeSpan.FromSeconds(_redisConfig.AuthExpiresIn));
            }
            return true;
        }
        //public async Task<bool> SetObjectAsync(string key, string field, object value, TimeSpan? expireTime = null)
        //{
        //    var result = string.IsNullOrWhiteSpace(field) ?
        //        await _db.SetAddAsync(_redisConfig.Prefix + key, value)
        //        : await _db.HashSetAsync(_redisConfig.Prefix + key, field, valueJson);
        //    if (expireTime != null)
        //    {
        //        await _db.KeyExpireAsync(_redisConfig.Prefix + key, expireTime);
        //    }
        //    else
        //    {
        //        await _db.KeyExpireAsync(_redisConfig.Prefix + key, TimeSpan.FromSeconds(_redisConfig.AuthExpiresIn));
        //    }
        //    return result;

        //}
    }


}
