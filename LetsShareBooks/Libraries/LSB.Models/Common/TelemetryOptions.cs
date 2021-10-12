using System.Diagnostics.CodeAnalysis;

namespace LSB.Models
{
    [ExcludeFromCodeCoverage]
    public class TelemetryOptions
    {
        public string EnvironmentName { get; set; }
        public string ComponentId { get; set; }
        public string ServiceLineName { get; set; }
        public string ServiceOfferingName { get; set; }
        public string ServiceName { get; set; }
        public string ComponentName { get; set; }
    }
}
