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
    }


    public class SANSService : BaseHttpClientService, ISANSService
    {
        Uri BaseURI = new Uri(AppConstants.ApplicationURL);

        public SANSService()
        {
            SetupAuthTokenHeader();
        }


        void SetupAuthTokenHeader()
        {
            AuthHeaders?.Clear();
            AuthHeaders = AuthHeaders ?? new Dictionary<string, string>();

            DefaultHeaders?.Clear();
            DefaultHeaders = DefaultHeaders ?? new Dictionary<string, string>();
        }


        async public Task<IList<Section>> GetOutline()
        {
            var url = new Uri(BaseURI, $"{ApiRoutes.Outline.Root}.json");
            var response = await SendRequestAsync<OutlineResult>(url, HttpMethod.Get, null, 30);
            return response?.Sections;
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
