using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using x2136443.DataModels;

namespace x2136443.Services.RemoveServices
{
    public interface ISANSService
    {
        Task<IList<Section>> GetOutline();
        Task<byte[]> DownloadVideoAtUrl(string url);
    }


    public class SANSService : BaseHttpClientService, ISANSService
    {
        Uri BaseURI = new Uri(AppConstants.ApplicationURL);

        public SANSService()
        {
            AuthHeaders?.Clear();
            AuthHeaders = AuthHeaders ?? new Dictionary<string, string>();

            DefaultHeaders?.Clear();
            DefaultHeaders = DefaultHeaders ?? new Dictionary<string, string>();
        }

        async protected override Task<Tuple<bool, T>> GetFromCache<T>(string key)
        {
            var obj = await App.PersistantStorageService.Get<T>(key);
            return new Tuple<bool, T>(obj != null, obj);
        }
        async protected override Task SetToCache<T>(string key, T obj)
        {
            await App.PersistantStorageService.Set(key, obj);
        }


        async public Task<IList<Section>> GetOutline()
        {
            var uri = new Uri(BaseURI, $"{ApiRoutes.Outline.Root}.json");
            var response = await SendRequestAsync<OutlineResult>(uri, HttpMethod.Get, null, true);
            return response?.Sections;
        }

        async public Task<byte[]> DownloadVideoAtUrl(string url)
        {
            var uri = new Uri(url);
            var response = await DownloadBytesAsync(uri);
            return response;
        }

        class ApiRoutes
        {
            public class Outline
            {
                public static string Root = "outline";
            }
        }
    }
}
