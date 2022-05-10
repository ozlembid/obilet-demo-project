using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OBilet.UI.Helpers
{
    public class CacheHelper
    {
        public static void SetKey<T>(string key, T Data, IMemoryCache _memoryCache)
        {
            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
            options.AbsoluteExpiration = DateTime.Now.AddDays(1);
            _memoryCache.Set<T>(key, Data, options);
        }

        public static T GetKey<T>(string key, IMemoryCache _memoryCache)
        {
            return _memoryCache.Get<T>(key);
        }
    }
}
