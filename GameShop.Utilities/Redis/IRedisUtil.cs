using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Utilities.Redis
{
    public interface IRedisUtil
    {
        Task<T> GetAsync<T>(string key, string field);

        Task<bool> SetAsync(string key, string field, string valueJson, TimeSpan? expireTime = null);
        Task<bool> RemoveAsync(string key, string field, bool isHasPrefix = false);
        Task<bool> RemoveRangeKeyAsync(List<string> keys, List<string> fields = null, bool isHasPrefix = false);
        Task<List<string>> HashGetAllAsync(string key);
        List<string> GetAllKeys(string searchText);
        Task<bool> SetMultiAsync(string key, HashEntry[] field, string valueJson, TimeSpan? expireTime = null);

        Task<T> GetOrAddAsync<T>(string key, string field, Func<Task<T>> func, TimeSpan? expireTime = null);
        //Task<bool> SetObjectAsync(string key, string field, RedisValue value, TimeSpan? expireTime = null);

    }
}
