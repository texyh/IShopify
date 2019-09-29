using System;
using System.Collections.Generic;
using System.Text;

namespace IShopify.Core.Framework
{
    public interface IRedisCacheService
    {
        void Add<T>(string key, T obj, TimeSpan cacheDuration);

        void Remove(string key);

        T Get<T>(string key);

        IList<T> GetAll<T>(IEnumerable<string> keys);
    }
}
