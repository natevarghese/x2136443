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

        protected async Task<T> SendRequestAsync<T>(Uri url, HttpMethod httpMethod, object requestData, int timeoutSeconds) where T : new()
        {
            // Default to GET
            var method = httpMethod ?? HttpMethod.Get;

            // Serialize request data (if any)
            string data = null;
            if (RequestDataSerializingDelegate != null && requestData != null)
            {
                data = RequestDataSerializingDelegate?.Invoke(requestData);
            }
            else
            {
                data = requestData == null ? null : JsonConvert.SerializeObject(requestData);
            }

            using (var request = new HttpRequestMessage(method, url))
            {
                if (data != null)
                {
                    request.Content = new StringContent(data, Encoding.UTF8, "application/json");
                }

                ApplyHeadersToRequst(request.Headers);


                try
                {
                    using (var handler = new HttpClientHandler())
                    using (var client = new HttpClient(handler) { Timeout = TimeSpan.FromSeconds(timeoutSeconds) })
                    using (var responseMsg = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead))
                        return await DeseralizeResponse<T>(responseMsg);
                }
                catch (HttpRequestException e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
                catch (System.Net.WebException e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
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