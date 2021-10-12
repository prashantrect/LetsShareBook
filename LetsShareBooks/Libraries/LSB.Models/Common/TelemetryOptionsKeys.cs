using System.Diagnostics.CodeAnalysis;

namespace LSB.Models
{
    [ExcludeFromCodeCoverage]
    public class TelemetryOptionsKeys
    {
        public const string EnvironmentName = "EnvironmentName";
        public const string ComponentId = "ComponentId";
        public const string ComponentName = "ComponentName";
        public const string ServiceName = "ServiceName";
        public const string ServiceLineName = "ServiceLineName";
        public const string ServiceOfferingName = "ServiceOfferingName";

        public const string CorrelationId = "CorrelationId";
        public const string ClientId = "ClientId";
    }
}
