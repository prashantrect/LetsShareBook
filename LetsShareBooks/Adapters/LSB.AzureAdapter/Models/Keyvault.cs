namespace LSB.AzureAdapter.Models
{
    public class Keyvault
    {
        public string Name { get; set; }
        public string BaseUrl { get; set; }
        public string AadClientId { get; set; }
        public string AadClientSecret { get; set; }
        public string Thumbprint { get; set; }
    }
}
