using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Register.APPLICATION.Interface;

namespace Register.APPLICATION.Common.Behaviors
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ICacheService _cache;

        public CachingBehavior(ICacheService cache)
        {
            _cache = cache;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is ICachableQuery cachableQuery)
            {
                string finalKey = cachableQuery.CacheKey;

                if (!string.IsNullOrEmpty(cachableQuery.VersionKey))
                {
                    int version = await _cache.GetAsync<int>(cachableQuery.VersionKey);
                    if (version == 0)
                    {
                        version = 1;
                        await _cache.SetAsync(cachableQuery.VersionKey, version, TimeSpan.FromDays(30));
                    }
                    finalKey = $"{cachableQuery.CacheKey}_v{version}";
                }
                
                var cachedResponse = await _cache.GetAsync<TResponse>(finalKey);
                if (cachedResponse != null)
                {
                    return cachedResponse;
                }

                var response = await next();

                if (response != null)
                {
                    var expiration = cachableQuery.Expiration ?? TimeSpan.FromMinutes(10);
                    await _cache.SetAsync(finalKey, response, expiration);
                }

                return response;
            }

            return await next();
        }
    }
}
