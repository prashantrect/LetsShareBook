using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LSB.Adapters
{
    public class RestClient : IRestClient
    {
        public string Name { get; }

        public HttpClient Client { get; set; }

        private readonly string _baseUrl;

        public RestClient(string name, string baseUrl, string resource, Func<string, Task<string>> tokenFunc) :  this(name, baseUrl, resource, new HttpTokenHandler(tokenFunc, resource))
        {
        }

        public RestClient(string name, string baseAddress, string resource, params DelegatingHandler[] handlers)
        {
            Name = name;
            _baseUrl = baseAddress;
            HttpClientHandler clientHandler = new();

            Client = HttpClientFactory.Create(clientHandler, handlers);
            Client.BaseAddress = new Uri(baseAddress);
        }

        public async Task<T> GetAsync<T>(string path)
        {
            var endpoint = _baseUrl + path;
            var response = await Client.GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                var content = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
                return content;
            }
            else
            {
                var servRes = await response.Content.ReadAsStringAsync();
                var ex = new HttpClientException(endpoint, response.StatusCode, servRes);
                throw ex;
            }
        }

        public async Task<string> GetStringAsync(string path)
        {
            var endpoint = _baseUrl + path;
            var response = await Client.GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            else
            {
                var servRes = await response.Content.ReadAsStringAsync();
                var ex = new HttpClientException(endpoint, response.StatusCode, servRes);
                throw ex;
            }
        }

        public async Task<HttpResponseMessage> GetStringAsync_V2(string path)
        {
            var endpoint = _baseUrl + path;
            var response = await Client.GetAsync(endpoint);
            return response;
        }

        public async Task<R> PostAsync<S, R>(string path, S value)
        {
            var response = await Client.PostAsJsonAsync(path, value);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<R>();
            }
            else
            {
                var servRes = await response.Content.ReadAsStringAsync();
                var ex = new HttpClientException(path, response.StatusCode, servRes);
                throw ex;
            }
        }

        public async Task<string> PostAsync(string path, string value)
        {
            var response = await Client.PostAsync(path, new StringContent(value, System.Text.Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
            else
            {
                var servRes = await response.Content.ReadAsStringAsync();
                var ex = new HttpClientException(path, response.StatusCode, servRes);
                throw ex;
            }
        }

        public async Task<HttpResponseMessage> PostAsync_V2(string path, string value)
        {
            HttpResponseMessage response = await Client.PostAsync(path, new StringContent(value, System.Text.Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                return response;
            }

            string servRes = await response.Content.ReadAsStringAsync();
            throw new HttpClientException(path, response.StatusCode, servRes);
        }

        public async Task<string> PostXmlAsync(string path, string value)
        {
            var response = await Client.PostAsync(path, new StringContent(value, System.Text.Encoding.UTF8, "application/xml"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
            else
            {
                var servRes = await response.Content.ReadAsStringAsync();
                var ex = new HttpClientException(path, response.StatusCode, servRes);
                throw ex;
            }
        }

        public async Task<T> PutAsync<T>(string path, string value)
        {
            var response = await Client.PutAsync(path, new StringContent(value, System.Text.Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<T>();
                return result;
            }
            else
            {
                var servRes = await response.Content.ReadAsStringAsync();
                var ex = new HttpClientException(path, response.StatusCode, servRes);
                throw ex;
            }
        }

        public async Task<R> PutAsync<S, R>(string path, S value)
        {
            var response = await Client.PutAsJsonAsync(path, value);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<R>();
            }
            else
            {
                var servRes = await response.Content.ReadAsStringAsync();
                var ex = new HttpClientException(path, response.StatusCode, servRes);
                throw ex;
            }
        }

        public virtual async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return await Client.SendAsync(request);
        }
    }
}
