using System.Diagnostics.CodeAnalysis;

namespace LSB.Models
{
    [ExcludeFromCodeCoverage]
    public class AzureADOptions
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Instance { get; set; }
        public string Domain { get; set; }
        public string TenantId { get; set; }
    }
}
