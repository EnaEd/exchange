using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Mobile.Core.Services
{
    public class BaseApiService
    {
        public async Task<HttpResponseMessage> ExecuteGetAsync<T>(string url)
        {
            HttpClient client = GetHttpCliet();
            HttpResponseMessage response = await client.GetAsync(url);
            return response;
        }

        public async Task<HttpResponseMessage> ExecutePostAsync<T>(string url, T data)
        {
            HttpClient client = GetHttpCliet();
            string content = JsonConvert.SerializeObject(data);
            HttpResponseMessage response = await client.PostAsync(url, new StringContent(content, Encoding.Unicode, "application/json"));
            return response;
        }

        public HttpClient GetHttpCliet()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}
