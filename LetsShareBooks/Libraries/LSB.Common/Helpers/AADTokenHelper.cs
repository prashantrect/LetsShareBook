//-----------------------------------------------------------------------
// <copyright file="AADTokenHelper.cs" company="Microsoft Corporation">
//     Copyright © Microsoft Corporation. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace LSB.Helpers
{
    [ExcludeFromCodeCoverage]
    public class AADTokenHelper
    {
        private string ClientId { get; set; }
        private string ClientSecret { get; set; }
        private string Authority { get; set; }

        public AADTokenHelper(string clientId, string clientSecret, string authority)
        {
            this.ClientId = clientId;
            this.ClientSecret = clientSecret;
            this.Authority = authority;
        }

        public async Task<string> GetAADTokenAsync(string audience)
        {
            try
            {
                var authenticationContext = new AuthenticationContext(this.Authority);
                var clientCredential = new ClientCredential(this.ClientId, this.ClientSecret);
                var bearerToken = (await authenticationContext.AcquireTokenAsync(audience, clientCredential)).AccessToken;
                return bearerToken;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
