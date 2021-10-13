using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSB.Constants
{
    public class ConfigKeys
    {
        #region Key Vault Config Constants

        public const string IsDevMode = "IsDevMode";

        public const string KVConfigSectionName = "KeyVaultConfig";

        public const string KeyVaultBaseUrl = "KeyVaultBaseUrl";

        public const string KeyVaultAadClientId = "ClientId";

        public const string KeyVaultThumbprint = "Thumbprint";

        #endregion

        #region Service AAD Config Section

        public const string AzureAdInstance = "AzureAdInstance";

        public const string AzureAdDomain = "AzureAdDomain";

        public const string AzureAdAudience = "AzureAdAudience";

        public const string AzureAdTenantId = "AzureAdTenantId";

        public const string AzureAdClientId = "AzureAdClientId";

        public const string AzureAdClientSecret = "AzureAdClientSecret";

        public const string AzureAdCallbackPath = "AzureAdCallbackPath";

        public const string LSBServiceBaseUrl = "LSBServiceBaseUrl";

        #endregion

        public const string ConfigSectionName = "LSBConfig";

        public const string LSBDBConnectionString = "LSBDBConnectionString";

        #region Application Insights Telemetry Keys

        public const string EnvironmentName = "EnvironmentName";

        public const string ComponentId = "ComponentId";

        public const string ComponentName = "ComponentName";

        public const string ServiceName = "ServiceName";

        public const string ServiceLineName = "ServiceLineName";

        public const string ServiceOfferingName = "ServiceOfferingName";

        public const string AppInsightsInstrumentKey = "AppInsightsInstrumentKey";

        #endregion
    }
}
