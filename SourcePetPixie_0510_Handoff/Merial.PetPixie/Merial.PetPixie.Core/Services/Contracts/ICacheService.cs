using System;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Models.Enums;

namespace Merial.PetPixie.Core.Services.Contracts
{
    public interface ICacheService
    {
        Task AddOrUpdate<T>(string key, T value, CacheMode mode = CacheMode.User);

        Task AddOrUpdate<T>(string key, T value, TimeSpan? expiration, CacheMode mode = CacheMode.User);

        Task<T> Get<T>(string key, CacheMode mode = CacheMode.User, bool rethrowError = false);

        T GetResult<T>(string key, CacheMode mode = CacheMode.User);

        Task Delete(string key, CacheMode mode = CacheMode.User);

        Task<T> GetOrFetch<T>(string key, Func<Task<T>> func, TimeSpan? expiration, CacheMode mode = CacheMode.User);
    }
}