using LSB.Constants;
using LSB.Contracts;
using LSB.Helpers;
using LSB.Models;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using LSB.AzureAdapter;

namespace LSB.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly ILogger Logger;

        public Startup(IConfiguration _configuration, ILogger<Startup> _logger)
        {
            Configuration = _configuration;
            Logger = _logger;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
            });
            services.AddMvc(option => option.EnableEndpointRouting = false).AddNewtonsoftJson();

            services.AddControllers();
            services.AddSwaggerGen();

            services.AddLogging();
            services.AddSingleton(Logger);

            RegisterDependencies(services);
            RegisterTelemetryDependencies(services);

            services.AddAuthentication(sharedOptions =>
            {
                sharedOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddAzureAdBearer(options => Configuration.Bind("AzureAd", options));

            services.AddHttpContextAccessor();
            services.AddScoped<ILoggedUser, LoggedUser>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyOrigin();
                x.AllowAnyMethod();
            });

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SCE Toolkit Service");
            });
        }

        public Func<string, Task<string>> GetTokenFunction(string clientId, string clientSecret, string authority)
        {
            var aadTokenHelper = new AADTokenHelper(clientId, clientSecret, authority);
            return aadTokenHelper.GetAADTokenAsync;
        }

        private void RegisterDependencies(IServiceCollection services)
        {
            services.AddSingleton(Configuration);

            services.AddScoped<ISqlDBManager>(x =>
            {
                return new SqlDBManager(Configuration[ConfigKeys.SCEToolKitDBConnectionString]);
            });

            services.AddScoped<ICosmosManager>(x =>
            {
                return new CosmosManager(Configuration[ConfigKeys.SCEToolKitDBConnectionString]);
            });

        }

        private void RegisterTelemetryDependencies(IServiceCollection services)
        {
            ApplicationInsightsServiceOptions aiOptions = new()
            {
                // Disables adaptive sampling.
                EnableAdaptiveSampling = false,
                // Disables QuickPulse (Live Metrics stream).
                EnableQuickPulseMetricStream = false,
                InstrumentationKey = Configuration[AIConfigKeys.AppInsightsInstrumentKey]
            };
            services.AddApplicationInsightsTelemetry(aiOptions);
            // Setting Telemetry values
            services.AddOptions<TelemetryOptions>().Configure<IConfiguration>((settings, configuration) =>
            {
                settings.EnvironmentName = Configuration[AIConfigKeys.EnvironmentName];
                settings.ComponentId = Configuration[AIConfigKeys.ComponentId];
                settings.ComponentName = Configuration[AIConfigKeys.ComponentName];
                settings.ServiceName = Configuration[AIConfigKeys.ServiceName];
                settings.ServiceLineName = Configuration[AIConfigKeys.ServiceLineName];
                settings.ServiceOfferingName = Configuration[AIConfigKeys.ServiceOfferingName];
            });
            services.AddSingleton<ITelemetryInitializer, ServiceTreeTelemetryInitializer>();
            services.AddSingleton<ITelemetryInitializer, AsyncLocalTelemetryInitializer>();
        }
    }
}
