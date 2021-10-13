using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LSB.Helpers
{
    [ExcludeFromCodeCoverage]
    public class AsyncLocalTelemetryInitializer : ITelemetryInitializer
    {
        public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();

        public void Initialize(ITelemetry telemetry)
        {
            if (telemetry is ISupportProperties propTelemetry)
            {
                if (Properties.Count > 0)
                {
                    foreach (KeyValuePair<string, string> kv in Properties)
                    {
                        if (propTelemetry.Properties.ContainsKey(kv.Key))
                        {
                            propTelemetry.Properties.Remove(kv.Key);
                        }

                        propTelemetry.Properties[kv.Key] = kv.Value;
                    }
                }
            }
        }
    }
}
