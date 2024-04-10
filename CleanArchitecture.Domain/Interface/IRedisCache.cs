using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Interface
{
    public interface IRedisCache
    {
        Task<T> GetCacheData<T>(string key);
        Task<object> RemoveData(string key);
        Task<bool> SetCacheData<T>(string key, T value, TimeSpan expirationTime);
    }
}
