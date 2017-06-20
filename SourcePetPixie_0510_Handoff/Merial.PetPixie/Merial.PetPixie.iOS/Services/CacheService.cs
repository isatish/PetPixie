using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using Merial.PetPixie.Core.Models.Enums;
using Merial.PetPixie.Core.Services.Contracts;

namespace Merial.PetPixie.iOS.Services
{
    public class CacheService :ICacheService
    {
        public CacheService()
        {
            BlobCache.ApplicationName = "Pet+Pixie";
            BlobCache.EnsureInitialized();
        }

        public async Task AddOrUpdate<T>(string key, T value, CacheMode mode = CacheMode.User)
        {
            await AddOrUpdate(key, value, null, mode);
        }

        public async Task AddOrUpdate<T>(string key, T value, TimeSpan? expiration, CacheMode mode = CacheMode.User)
        {
            var cache = GetCache(mode);
            if (expiration.HasValue && expiration != TimeSpan.MaxValue)
            {
                await cache.InsertObject(key, value, expiration.Value);
            }
            else {
                await cache.InsertObject(key, value);
            }
        }

        public async Task<T> Get<T>(string key, CacheMode mode = CacheMode.User, bool rethrowError = false)
        {
            var cache = GetCache(mode);
            try
            {
                return await cache.GetObject<T>(key);
            }
            catch (Exception)
            {
                if (rethrowError)
                    throw;
                return default(T);
            }
        }

        public T GetResult<T>(string key, CacheMode mode = CacheMode.User)
        {
            var cache = GetCache(mode);
            try
            {
                return cache.GetObject<T>(key).First();
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public async Task<T> GetOrFetch<T>(string key, Func<Task<T>> func, TimeSpan? expiration, CacheMode mode = CacheMode.User)
        {
            var cache = GetCache(mode);
            DateTimeOffset? timeOffset = null;
            if (expiration.HasValue)
            {
                timeOffset = expiration == TimeSpan.MaxValue ? DateTimeOffset.MaxValue : DateTimeOffset.Now + expiration.Value;
            }

            return await cache.GetOrFetchObject(key, func, timeOffset).FirstOrDefaultAsync();
        }

        public async Task Delete(string key, CacheMode mode = CacheMode.User)
        {
            var cache = GetCache(mode);
            await cache.Invalidate(key);
        }

        private IBlobCache GetCache(CacheMode mode)
        {
            switch (mode)
            {
                case CacheMode.Device:
                    return BlobCache.LocalMachine;
                case CacheMode.InMemory:
                    return BlobCache.InMemory;
                case CacheMode.User:
                    return BlobCache.UserAccount;

                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

      
    }
}