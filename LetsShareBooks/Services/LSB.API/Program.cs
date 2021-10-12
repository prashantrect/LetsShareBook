using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using LSB.AzureAdapter;
using LSB.AzureAdapter.Models;
using LSB.Constants;

namespace LSB.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
             WebHost.CreateDefaultBuilder(args)
                 .ConfigureAppConfiguration((context, config) =>
                 {
                     var builtConfig = config.Build();
                     Keyvault keyvault = new Keyvault()
                     {
                         BaseUrl = builtConfig[KVConfigKeys.KeyVaultBaseUrl]
                     };
                     //For local development, please set the IsDevMode field to true and for deployment, please set to false inside appsettings.json file
                     IKeyvaultManager keyvaultManager = new KeyvaultManager(keyvault, bool.Parse(builtConfig[KVConfigKeys.IsDevMode]));
                     config.AddInMemoryCollection(keyvaultManager.FetchKeyvaultSecretsList());
                 })
                .ConfigureLogging(logging =>
                {
                })
                 .UseStartup<Startup>()
                 .Build();
    }
}
