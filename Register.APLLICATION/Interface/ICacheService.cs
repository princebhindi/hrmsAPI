using System;
using System.Threading.Tasks;

namespace Register.APPLICATION.Interface
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value, TimeSpan? expirationTime = null);
        Task RemoveAsync(string key);
    }
}
