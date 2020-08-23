using Newtonsoft.Json;
using SampleAPI.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SampleAPI.BLL.Services
{
    [Export(typeof(IHttpClientService)), PartCreationPolicy(CreationPolicy.Shared)]
    public class HttpClientService : IHttpClientService
    {
        public HttpClientService()
        {
        }

        public async Task<T> Get<T>(Uri requestUri, string userName, string password)
        {
            T result = default;

            using (var client = new HttpClient())
            {
                HttpResponseMessage message = default;
                byte[] credentials = Encoding.UTF8.GetBytes($"{userName}:{password}");
                string b64Credentials = Convert.ToBase64String(credentials);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("basic", b64Credentials);
                message = await client.GetAsync(requestUri);

                if (message.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<T>(await message.Content.ReadAsStringAsync());
                }
            }

            return result;
        }

        public async Task<T> Post<T>(Uri requestUri, string userName, string password)
        {
            T result = default;

            using (var client = new HttpClient())
            {
                HttpResponseMessage message = default;
                byte[] credentials = Encoding.UTF8.GetBytes($"{userName}:{password}");
                string b64Credentials = Convert.ToBase64String(credentials);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("basic", b64Credentials);
                message = await client.PostAsync(requestUri, default);

                if (message.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<T>(await message.Content.ReadAsStringAsync());
                }
            }

            return result;
        }
    }
}
