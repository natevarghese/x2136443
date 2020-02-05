using System.Threading.Tasks;
using Akavache;
using System.Reactive.Linq;
using System;

namespace x2136443.Services
{
    public interface IPersistantStorageService
    {
        Task<T> GetUserSetting<T>(string key);
        Task SetUserSetting(string key, object value);
        Task<T> Get<T>(string key);
        Task Set(string key, object value);
        Task Clear();
    }


    public class PersistantStorageService : IPersistantStorageService
    {
        async public Task Clear()
        {
            await BlobCache.UserAccount.InvalidateAll();
            await BlobCache.LocalMachine.InvalidateAll();
        }

        async public Task SetUserSetting(string key, object value)
        {
            try
            {
                await BlobCache.UserAccount.InsertObject(key, value);
            }
            catch
            {

            }
        }

        async public Task<T> GetUserSetting<T>(string key)
        {
            try
            {
                return await BlobCache.UserAccount.GetObject<T>(key).Catch(Observable.Return(default(T)));
            }
            catch
            {
                return default(T);
            }
        }

        async public Task Set(string key, object value)
        {
            try
            {
                await BlobCache.LocalMachine.InsertObject(key, value);
            }
            catch
            {
            }
        }

        async public Task<T> Get<T>(string key)
        {
            try
            {
                return await BlobCache.LocalMachine.GetObject<T>(key);
            }
            catch
            {
                return default(T);
            }
        }
    }
}