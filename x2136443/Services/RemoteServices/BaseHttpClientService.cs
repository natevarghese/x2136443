using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace x2136443.Services.RemoveServices
{
    public class BaseHttpClientService
    {
        public IDictionary<string, string> DefaultHeaders { get; set; }
        public IDictionary<string, string> AuthHeaders { get; set; }
        public Func<object, string> RequestDataSerializingDelegate { get; set; }

        static HttpClient Client = new HttpClient(new HttpClientHandler());

        protected async Task<T> SendRequestAsync<T>(Uri url, HttpMethod httpMethod, object requestData, bool isCacheEnabled) where T : new()
        {

            // Default to GET
            var method = httpMethod ?? HttpMethod.Get;

            //deal with cache
            var cacheKey = url?.OriginalString;
            if (isCacheEnabled)
            {
                var cachedResult = await GetFromCache<T>(cacheKey);
                if (cachedResult.Item1)
                    return cachedResult.Item2;
            }

            // Serialize request data (if any)
            string data = null;
            if (RequestDataSerializingDelegate != null && requestData != null)
                data = RequestDataSerializingDelegate?.Invoke(requestData);
            else
                data = requestData == null ? null : JsonConvert.SerializeObject(requestData);

            using (var request = new HttpRequestMessage(method, url))
            {
                if (data != null)
                    request.Content = new StringContent(data, Encoding.UTF8, "application/json");

                ApplyHeadersToRequst(request.Headers);

                try
                {
                    using (var responseMsg = await Client.SendAsync(request, HttpCompletionOption.ResponseContentRead))
                    {
                        var responseObj = await DeseralizeResponse<T>(responseMsg);

                        if (isCacheEnabled)
                            await SetToCache(cacheKey, responseObj);

                        return responseObj;
                    }
                }
                catch (TaskCanceledException) { }
                catch (NullReferenceException) { }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
            }

            return default(T);
        }
        protected virtual Task<Tuple<bool, T>> GetFromCache<T>(string key) where T : new()
        {
            return Task.FromResult(new Tuple<bool, T>(false, default(T)));
        }
        protected virtual Task SetToCache<T>(string key, T obj) where T : new()
        {
            return Task.FromResult(default(T));
        }
        protected async Task<byte[]> DownloadBytesAsync(Uri uri)
        {
            try
            {
                using (var httpResponse = await Client.GetAsync(uri))
                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                        return await httpResponse.Content.ReadAsByteArrayAsync();
            }
            catch { }

            return null;
        }

        protected virtual void ApplyHeadersToRequst(HttpRequestHeaders headers) { }

        async virtual protected Task<T> DeseralizeResponse<T>(HttpResponseMessage httpResponseMessage)
        {
            var content = httpResponseMessage?.Content == null ? null : await httpResponseMessage.Content.ReadAsStringAsync();

            System.Diagnostics.Debug.WriteLine(content);

            if (string.IsNullOrWhiteSpace(content))
                return default(T);

            if (!httpResponseMessage.IsSuccessStatusCode)
                return default(T);

            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}