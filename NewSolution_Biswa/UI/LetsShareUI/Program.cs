using LSB.AzureAdapter;
using LSB.AzureAdapter.Models;
using LSB.Constants;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace LetsShareUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    var builtConfig = config.Build();
                    Keyvault keyvault = new Keyvault()
                    {
                        BaseUrl = builtConfig[ConfigKeys.KeyVaultBaseUrl]
                    };
                    //For local development, please set the IsDevMode field to true and for deployment, please set to false inside appsettings.json file
                    IKeyvaultManager keyvaultManager = new KeyvaultManager(keyvault, bool.Parse(builtConfig[ConfigKeys.IsDevMode]));
                    config.AddInMemoryCollection(keyvaultManager.FetchKeyvaultSecretsList());
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
