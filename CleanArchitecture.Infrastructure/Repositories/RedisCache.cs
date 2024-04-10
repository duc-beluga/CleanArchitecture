using CleanArchitecture.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using Newtonsoft.Json;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class RedisCache : IRedisCache
    {
        private readonly IDatabase _redisDB;

        public RedisCache(IConnectionMultiplexer connectionMultiplexer)
        {
            _redisDB = connectionMultiplexer.GetDatabase();
        }

        public async Task<T> GetCacheData<T>(string key)
        {
            var value = await _redisDB.StringGetAsync(key);
            if (!string.IsNullOrEmpty(value))
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            return default;
        }

        public async Task<object> RemoveData(string key)
        {
            bool keyExist = await _redisDB.KeyExistsAsync(key);
            if (keyExist)
            {
                return _redisDB.KeyDelete(key);
            }
            return false;
        }

        public async Task<bool> SetCacheData<T>(string key, T value, TimeSpan expirationTime)
        {
            var isSet = await _redisDB.StringSetAsync(key, JsonConvert.SerializeObject(value), expirationTime);
            return isSet;
        }
    }
}
