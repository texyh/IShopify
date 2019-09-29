using IShopify.Core.Framework;
using IShopify.Core.Helpers;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IShopify.Framework
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDistributedCache _cache;

        public RedisCacheService(IDistributedCache distributedCache)
        {
            _cache = distributedCache;
        }

        public void Add<T>(string key, T obj, TimeSpan cacheDuration)
        {

            _cache.SetString(
                    key, 
                    obj.ToJson(), 
                    new DistributedCacheEntryOptions {
                        AbsoluteExpirationRelativeToNow = cacheDuration 
                    }
                );
        }

        public T Get<T>(string key)
        {
            var obj = _cache.GetString(key);
            return obj.IsNull() ? default(T) : obj.FromJson<T>();
        }

        public IList<T> GetAll<T>(IEnumerable<string> keys)
        {
            var values = new List<T>(keys.Count());

            foreach (var key in keys)
            {
                var obj = _cache.GetString(key);
                if(!obj.IsNull())
                {
                    values.Add(obj.FromJson<T>());
                }
            }

            return values;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}
