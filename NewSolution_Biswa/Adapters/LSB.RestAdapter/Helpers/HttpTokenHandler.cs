using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LSB.Adapters
{
    [ExcludeFromCodeCoverage]
    public class HttpTokenHandler : DelegatingHandler
    {
        private readonly Func<string, Task<string>> _tokenFunc;

        private readonly string _resource;

        public HttpTokenHandler(Func<string, Task<string>> tokenFunc, string resource)
        {
            this._tokenFunc = tokenFunc;
            this._resource = resource;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            var token = await this._tokenFunc(_resource);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
