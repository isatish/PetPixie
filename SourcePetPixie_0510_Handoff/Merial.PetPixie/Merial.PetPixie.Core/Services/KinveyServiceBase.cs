using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Logging;
using KinveyXamarin;
using Merial.PetPixie.Core.Models.Enums;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.Services.Contracts;
using MvvmCross.Platform;
using Newtonsoft.Json;

namespace Merial.PetPixie.Core.Services
{
    public class KinveyServiceBase
    {
        protected readonly ILog _logger;
        protected readonly IKinveyService KinveyService;
        private readonly ICacheService _cacheService;
        public KinveyServiceBase(IKinveyService kinveyService)
        {
            KinveyService = kinveyService;
            _logger = Mvx.Resolve<ILog>();
            _cacheService = Mvx.Resolve<ICacheService>();
        }




        public async Task<T> GetAppDataEntityAsync<T>(string endpoint, string entityId)
        {
            AsyncAppData<T> appData = KinveyService.GetClient().AppData<T>(endpoint, typeof(T));

            var result = await appData.GetEntityAsync(entityId);
            return result;
        }


        public async Task<IEnumerable<T>> GetAppDataEntitesAsync<T>(string endpoint, IEnumerable<string> entitiesId)
        {
            AsyncAppData<T> appData = KinveyService.GetClient().AppData<T>(endpoint, typeof(T));
            var result = await appData.GetAsync("{\"_id\":{\"$in\":[" + String.Join(",", entitiesId.Select(id => $"\"{id}\"")) + "]}}");
            return result.ToList();
        }

        public AsyncAppData<T> GetAppDataEntityAsync<T>(string endpoint)
        {
            return KinveyService.GetClient().AppData<T>(endpoint, typeof(T));
        }

        public async Task<List<T>> GetAppDataAsync<T>(string endpoint, object query = null)
        {
            AsyncAppData<T> appData = KinveyService.GetClient().AppData<T>(endpoint, typeof(T));

            if (query != null)
            {
                string jsonQuery = JsonConvert.SerializeObject(query);

                var result = await appData.GetAsync(jsonQuery);
                return result.ToList();
            }
            else
            {
                var result = await appData.GetAsync();
                return result.ToList();
            }
        }



        public async Task<List<T>> GetAppDataAsync<T>(string endpoint, string query)
        {
            AsyncAppData<T> appData = KinveyService.GetClient().AppData<T>(endpoint, typeof(T));

            if (!string.IsNullOrWhiteSpace(query))
            {

                var result = await appData.GetAsync(query);
                return result.ToList();
            }
            else
            {

                var result = await appData.GetAsync();
                return result.ToList();
            }
        }

        public AsyncAppData<T> GetAppDataQuery<T>(string endpoint)
        {
            return KinveyService.GetClient().AppData<T>(endpoint, typeof(T));
        }

        public Task<T> SaveAppdataAsync<T>(string endpoint, T body)
        {
            TaskCompletionSource<T> tsc = new TaskCompletionSource<T>();
            AsyncAppData<T> appData = KinveyService.GetClient().AppData<T>(endpoint, typeof(T));
            appData.Save(body, new KinveyDelegate<T>()
            {
                onSuccess = (response) =>
                {
                    tsc.SetResult(response);
                },
                onError = (error) =>
                {
                    tsc.SetException(error);
                }
            });
            return tsc.Task;
        }





        public Task<T> PostRPCAsync<T>(string endpoint, object body)
        {
            TaskCompletionSource<T> tsc = new TaskCompletionSource<T>();

            var endpoints = KinveyService.GetClient().CustomEndpoint<object, T>();
            endpoints.ExecuteCustomEndpoint(endpoint, body, new KinveyDelegate<T>()
            {
                onSuccess = (response) =>
                {
                    tsc.SetResult(response);
                },
                onError = (error) =>
                {
                    tsc.SetException(error);
                }
            });
            return tsc.Task;
        }

        public async Task<bool> DeleteAppdataAsync<T>(string endpoint, string entityId)
        {
            AsyncAppData<T> appData = KinveyService.GetClient().AppData<T>(endpoint, typeof(T));

            var result = await appData.DeleteAsync(entityId);
            return result.count == 1;
        }

        public Task<T> Execute<T>(string key, Func<Task<T>> func, LocalCachingPolicy cachingPolicy)
        {
            return Execute(key, func, cachingPolicy, RequestErrorHandlingPolicy.SilentErrorReturnNull);
        }

        public async Task<T> Execute<T>(string key, Func<Task<T>> func, LocalCachingPolicy cachingPolicy, RequestErrorHandlingPolicy errorPolicy)
        {
            var cachedResponse = default(T);

            try
            {
                Task<T> taskResult = ExecuteCore(key, func, cachingPolicy);
                return await taskResult;
            }
            catch (Exception ex)
            {
                switch (errorPolicy)
                {
                    case RequestErrorHandlingPolicy.SilentErrorReturnNull:
                        return default(T);
                    case RequestErrorHandlingPolicy.ThrowErrorOnStack:
                        throw ex;
                    default:
                        throw ex;
                }
                await _cacheService.Delete(key, CacheMode.Device);
                //throw ex;
            }

            return cachedResponse;
        }

        private async Task<T> ExecuteCore<T>(string key, Func<Task<T>> func, LocalCachingPolicy cachingPolicy)
        {
            var expiration = Config.CachePersitenceConfig.GetPersistance(cachingPolicy);

            return await _cacheService.GetOrFetch(key, func, expiration, CacheMode.Device);
        }
    }
}