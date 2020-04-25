using System;
using Microsoft.Extensions.Caching.Memory;

namespace SteamGameStatistics.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        public CacheService(IMemoryCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public void Create(string key, object data)
        {
            _cache.Set(key, data);
        }

        public void Delete(string key)
        {
            _cache.Remove(key);
        }

        public object TryGet(string key)
        {
            _cache.TryGetValue(key, out object cacheResults);

            return cacheResults;
        }
    }
}
