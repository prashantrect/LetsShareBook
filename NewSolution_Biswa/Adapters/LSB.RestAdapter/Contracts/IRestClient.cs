using System.Net.Http;
using System.Threading.Tasks;

namespace LSB.Adapters
{
    public interface IRestClient
    {
        #region GET Section

        Task<T> GetAsync<T>(string path);

        Task<string> GetStringAsync(string path);

        Task<HttpResponseMessage> GetStringAsync_V2(string path);

        #endregion

        #region POST Section


        Task<R> PostAsync<S, R>(string path, S value);

        Task<string> PostAsync(string path, string value);

        Task<string> PostXmlAsync(string path, string value);

        Task<HttpResponseMessage> PostAsync_V2(string path, string value);

        #endregion

        #region PUT Section


        Task<T> PutAsync<T>(string path, string value);

        Task<R> PutAsync<S, R>(string path, S value);

        #endregion
    }
}
