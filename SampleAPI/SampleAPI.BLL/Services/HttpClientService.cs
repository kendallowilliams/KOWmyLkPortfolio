using Newtonsoft.Json;
using SampleAPI.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SampleAPI.BLL.Services
{
    [Export(typeof(IHttpClientService)), PartCreationPolicy(CreationPolicy.Shared)]
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient httpClient;

        public HttpClientService()
        {
            httpClient = new HttpClient();
        }

        public async Task<T> Get<T>(Uri requestUri)
        {
            HttpResponseMessage message = await httpClient.GetAsync(requestUri);
            T result = default;

            if (message.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<T>(await message.Content.ReadAsStringAsync());
            }

            return result;
        }

        public async Task<T> Post<T>(Uri requestUri)
        {
            HttpResponseMessage message = await httpClient.PostAsync(requestUri, default);
            T result = default;

            if (message.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<T>(await message.Content.ReadAsStringAsync());
            }

            return result;
        }

        public void Dispose()
        {
            httpClient?.Dispose();
        }
    }
}
