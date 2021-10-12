using LSB.Models;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;

namespace LSB.Helpers
{
    [ExcludeFromCodeCoverage]
    public class ServiceTreeTelemetryInitializer : ITelemetryInitializer
    {
        private readonly IOptions<TelemetryOptions> _options;
        public ServiceTreeTelemetryInitializer(IOptions<TelemetryOptions> options)
        {
            _options = options;
        }

        public void Initialize(ITelemetry telemetry)
        {
            if (!telemetry.Context.GlobalProperties.ContainsKey(TelemetryOptionsKeys.ComponentId))
            {
                telemetry.Context.GlobalProperties[TelemetryOptionsKeys.ComponentId] = _options.Value.ComponentId;
                telemetry.Context.GlobalProperties[TelemetryOptionsKeys.ComponentName] = _options.Value.ComponentName;
                telemetry.Context.GlobalProperties[TelemetryOptionsKeys.EnvironmentName] = _options.Value.EnvironmentName;
                telemetry.Context.GlobalProperties[TelemetryOptionsKeys.ServiceLineName] = _options.Value.ServiceLineName;
                telemetry.Context.GlobalProperties[TelemetryOptionsKeys.ServiceName] = _options.Value.ServiceName;
                telemetry.Context.GlobalProperties[TelemetryOptionsKeys.ServiceOfferingName] = _options.Value.ServiceOfferingName;
            }
        }
    }
}
