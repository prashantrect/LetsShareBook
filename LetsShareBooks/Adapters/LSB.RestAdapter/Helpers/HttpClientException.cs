using Newtonsoft.Json;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;

namespace LSB.Adapters
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class HttpClientException : Exception
    {
        public HttpClientException(string uri, HttpStatusCode status, string message) : base(JsonConvert.SerializeObject(new { Uri = uri, StatusCode = status, Message = message }))
        {
            this.Status = status;
        }

        protected HttpClientException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public HttpStatusCode Status { get; }
    }
}
