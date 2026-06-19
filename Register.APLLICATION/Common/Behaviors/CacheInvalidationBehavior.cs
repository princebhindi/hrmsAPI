using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Register.APPLICATION.Interface;

namespace Register.APPLICATION.Common.Behaviors
{
    public class CacheInvalidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ICacheService _cache;

        public CacheInvalidationBehavior(ICacheService cache)
        {
            _cache = cache;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var response = await next();

            if (request is IInvalidateCache invalidator)
            {
                if (invalidator.InvalidateKeys != null)
                {
                    foreach (var key in invalidator.InvalidateKeys)
                    {
                        await _cache.RemoveAsync(key);
                    }
                }

                if (invalidator.InvalidateVersions != null)
                {
                    foreach (var versionKey in invalidator.InvalidateVersions)
                    {
                        int currentVersion = await _cache.GetAsync<int>(versionKey);
                        await _cache.SetAsync(versionKey, currentVersion + 1, TimeSpan.FromDays(30));
                    }
                }
            }

            return response;
        }
    }
}
