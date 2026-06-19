using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Register.APPLICATION.Interface;

namespace Register.PERSISTANCE.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger<CacheService> _logger;

        public CacheService(IDistributedCache cache, ILogger<CacheService> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            try
            {
                var cachedValue = await _cache.GetStringAsync(key);
                if (string.IsNullOrEmpty(cachedValue))
                {
                    return default;
                }

                return JsonSerializer.Deserialize<T>(cachedValue);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Redis connection failed or timed out during GET for key '{Key}'. Falling back to Database.", key);
                return default;
            }
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expirationTime = null)
        {
            try
            {
                var options = new DistributedCacheEntryOptions();
                if (expirationTime.HasValue)
                {
                    options.AbsoluteExpirationRelativeToNow = expirationTime.Value;
                }
                else
                {
                    options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5); // default 5 minutes
                }

                var serialized = JsonSerializer.Serialize(value);
                await _cache.SetStringAsync(key, serialized, options);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Redis connection failed or timed out during SET for key '{Key}'. Skipping cache insert.", key);
            }
        }

        public async Task RemoveAsync(string key)
        {
            try
            {
                await _cache.RemoveAsync(key);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Redis connection failed or timed out during REMOVE for key '{Key}'. Skipping cache invalidation.", key);
            }
        }
    }
}
