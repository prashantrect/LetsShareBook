using LSB.Constants;
using LSB.Helpers;
using LSB.Models;
using LSB.RestAdapter;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace LetsShareUI
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(AzureADDefaults.AuthenticationScheme)
                .AddAzureAD(options =>
                {
                    options.Instance = _configuration[ConfigKeys.AzureAdInstance];
                    options.Domain = _configuration[ConfigKeys.AzureAdDomain];
                    options.TenantId = _configuration[ConfigKeys.AzureAdTenantId];
                    options.ClientId = _configuration[ConfigKeys.AzureAdClientId];
                    options.CallbackPath = _configuration[ConfigKeys.AzureAdCallbackPath];
                });

            services.AddControllersWithViews(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });
            services.AddRazorPages();
            //services.AddSwaggerGen();
            services.AddLogging();

            RegisterDependencies(services);
            RegisterLoggingTelemetry(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Scrap}/{action=Upload}/{id?}");
                endpoints.MapRazorPages();
            });
        }

        public Func<string, Task<string>> GetTokenFunction(string clientId, string clientSecret, string authority)
        {
            var aadTokenHelper = new AADTokenHelper(clientId, clientSecret, authority);
            return aadTokenHelper.GetAADTokenAsync;
        }

        private void RegisterDependencies(IServiceCollection services)
        {
            services.AddSingleton(_configuration);

            services.AddSingleton<ILSBServiceAdapter>(new LSBServiceAdapter(
                "ScrapEvidenceService",
                _configuration[ConfigKeys.LSBServiceBaseUrl],
                _configuration[ConfigKeys.AzureAdAudience],
                GetTokenFunction(
                    _configuration[ConfigKeys.AzureAdClientId],
                    _configuration[ConfigKeys.AzureAdClientSecret],
                    string.Concat(_configuration[ConfigKeys.AzureAdInstance], _configuration[ConfigKeys.AzureAdDomain]))));
        }

        private void RegisterLoggingTelemetry(IServiceCollection services)
        {
            ApplicationInsightsServiceOptions aiOptions = new ApplicationInsightsServiceOptions
            {
                // Disables adaptive sampling.
                EnableAdaptiveSampling = false,
                // Disables QuickPulse (Live Metrics stream).
                EnableQuickPulseMetricStream = false,
                InstrumentationKey = _configuration[ConfigKeys.AppInsightsInstrumentKey]
            };
            services.AddApplicationInsightsTelemetry(aiOptions);
            // Setting Telemetry values
            services.AddOptions<TelemetryOptions>().Configure<IConfiguration>((settings, configuration) =>
            {
                settings.EnvironmentName = _configuration[ConfigKeys.EnvironmentName];
                settings.ServiceLineName = _configuration[ConfigKeys.ServiceLineName];
                settings.ServiceOfferingName = _configuration[ConfigKeys.ServiceOfferingName];
                settings.ServiceName = _configuration[ConfigKeys.ServiceName];
                settings.ComponentName = _configuration[ConfigKeys.ComponentName];
                settings.ComponentId = _configuration[ConfigKeys.ComponentId];
            });
            services.AddSingleton<ITelemetryInitializer, ServiceTreeTelemetryInitializer>();
            services.AddSingleton<ITelemetryInitializer, AsyncLocalTelemetryInitializer>();
        }
    }
}
