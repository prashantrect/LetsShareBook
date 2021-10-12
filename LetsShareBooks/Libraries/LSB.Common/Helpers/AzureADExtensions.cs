//-----------------------------------------------------------------------
// <copyright file="AzureADExtensions.cs" company="Microsoft Corporation">
//     Copyright © Microsoft Corporation. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using LSB.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.AspNetCore.Authentication
{
    [ExcludeFromCodeCoverage]
    public static class AzureAdServiceCollectionExtensions
    {
        public static AuthenticationBuilder AddAzureAdBearer(this AuthenticationBuilder builder) =>
            builder.AddAzureAdBearer(_ =>
            {
            });

        public static AuthenticationBuilder AddAzureAdBearer(this AuthenticationBuilder builder, Action<AzureADOptions> configureOptions)
        {
            builder.Services.Configure(configureOptions);
            builder.Services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureAzureOptions>();
            builder.AddJwtBearer();
            return builder;
        }

        private class ConfigureAzureOptions : IConfigureNamedOptions<JwtBearerOptions>
        {
            private readonly AzureADOptions _azureOptions;

            public ConfigureAzureOptions(IOptions<AzureADOptions> azureOptions)
            {
                _azureOptions = azureOptions.Value;
            }

            public void Configure(string name, JwtBearerOptions options)
            {
                options.Authority = $"{_azureOptions.Instance}{_azureOptions.TenantId}";

                // The valid audiences are both the Client ID(options.Audience) and api://{ClientID}
                options.TokenValidationParameters.ValidAudiences = new string[] { _azureOptions.ClientId, $"api://{_azureOptions.ClientId}" };
            }

            public void Configure(JwtBearerOptions options)
            {
                Configure(Options.DefaultName, options);
            }
        }
    }
}