using Azure.Core.Pipeline;
using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System;
using System.Net;
using System.Net.Http;

namespace LSB.AzureAdapter.Helpers
{
    public class GraphHelper
    {
        public GraphHelper()
        {

        }

        public GraphServiceClient CreateGraphClient(string[] graphScopes = null)
        {
            try
            {
                IPublicClientApplication publicClientApplication = PublicClientApplicationBuilder.Create("INSERT-CLIENT-APP-ID").Build();

                if (graphScopes == null || graphScopes.Length <= 0)
                {
                    var scopes = new[] { "https://graph.microsoft.com/.default" };
                }

                // Create an authentication provider by passing in a client application and graph scopes.
                DeviceCodeProvider authProvider = new DeviceCodeProvider(publicClientApplication, graphScopes);
                // Create a new instance of GraphServiceClient with the authentication provider.
                GraphServiceClient graphClient = new GraphServiceClient(authProvider);

                return graphClient;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public GraphServiceClient CreateGraphClientWithClientCredentials(string[] graphScopes = null)
        {
            string proxyAddress = ""; // "http://localhost:8888";
            HttpClientHandler handler = new HttpClientHandler();
            if(!string.IsNullOrEmpty(proxyAddress))
            {
                handler.Proxy = new WebProxy(new Uri(proxyAddress));
            }
            var options = new ClientSecretCredentialOptions
            {
                // Create a new Azure.Core.HttpClientTransport
                Transport = new HttpClientTransport(handler)
            };

            var credential = new ClientSecretCredential(
                "YOUR_TENANT_ID",
                "YOUR_CLIENT_ID",
                "YOUR_CLIENT_SECRET",
                options
            );

            if(graphScopes == null || graphScopes.Length <= 0)
            {
                graphScopes = new[] { "https://graph.microsoft.com/.default" };
            }
            var httpClient = !string.IsNullOrEmpty(proxyAddress)? GraphClientFactory.Create(new TokenCredentialAuthProvider(credential, graphScopes), proxy: new WebProxy(new Uri(proxyAddress))): GraphClientFactory.Create(new TokenCredentialAuthProvider(credential, graphScopes));
            GraphServiceClient graphClient = new(httpClient);

            return graphClient;
        }
    }
}
