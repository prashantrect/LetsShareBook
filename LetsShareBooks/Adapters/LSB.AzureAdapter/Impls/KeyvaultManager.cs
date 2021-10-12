using Azure;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using LSB.AzureAdapter.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LSB.AzureAdapter
{
    public class KeyvaultManager : IKeyvaultManager
    {
        public SecretClient Client { get; set; }

        public KeyvaultManager(Keyvault keyvault, bool isDevMode = false)
        {
            var defaultAzureCredentialOptions = new DefaultAzureCredentialOptions
            {
                ExcludeAzureCliCredential = false,
                ExcludeEnvironmentCredential = true,
                ExcludeInteractiveBrowserCredential = true,
                ExcludeManagedIdentityCredential = true,
                ExcludeSharedTokenCacheCredential = true,
                ExcludeVisualStudioCodeCredential = true,
                ExcludeVisualStudioCredential = true
            };

            DefaultAzureCredential azureCredential = new(new DefaultAzureCredentialOptions());
            if (isDevMode)
            {
                azureCredential = new DefaultAzureCredential(defaultAzureCredentialOptions);
            }
            Client = new SecretClient(new Uri(keyvault.BaseUrl), azureCredential);
        }

        public string FetchKeyvaultSecret(string secretName)
        {
            try
            {
                KeyVaultSecret secret = Client.GetSecret(secretName);
                return secret.Value.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public string SetKeyvaultSecret(string secretName, string secretValue)
        {
            try
            {
                KeyVaultSecret secret = Client.SetSecret(secretName, secretValue);
                return secret.Value.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public string UpdateKeyvaultSecret(string secretName, string secretValue)
        {
            try
            {
                KeyVaultSecret secret = Client.GetSecret(secretName);
                secret.Properties.ContentType = "text/plain";
                SecretProperties updatedSecretProperties = Client.UpdateSecretProperties(secret.Properties);
                return secret.Value.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public string DeleteOrPurgeKeyvaultSecret(string secretName, bool isPurge)
        {
            try
            {
                DeletedSecret secret = null;
                DeleteSecretOperation operation = Client.StartDeleteSecret("secret-name");
                if (isPurge)
                {
                    while (!operation.HasCompleted)
                    {
                        Thread.Sleep(2000);
                        operation.UpdateStatus();
                    }
                    secret = operation.Value;
                    Client.PurgeDeletedSecret(secret.Name);
                }
                else
                {
                    secret = operation.Value;
                }
                return secret.Value.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public Dictionary<string, string> FetchKeyvaultSecretsList()
        {
            Dictionary<string, string> allSecretsList = new();
            try
            {
                Pageable<SecretProperties> allSecrets = Client.GetPropertiesOfSecrets();
                foreach (SecretProperties secretProperties in allSecrets)
                {
                    string secretName = secretProperties.Name;
                    var secretData = Client.GetSecret(secretName);
                    string secretValue = secretData.Value.Value.ToString();
                    allSecretsList.Add(secretName, secretValue);
                }
            }
            catch (Exception)
            {
                allSecretsList = new Dictionary<string, string>();
            }
            return allSecretsList;
        }

        public async Task<Dictionary<string, string>> FetchKeyvaultSecretsListAsync()
        {
            Dictionary<string, string> allSecretsList = new();
            try
            {
                AsyncPageable<SecretProperties> allSecrets = Client.GetPropertiesOfSecretsAsync();
                await foreach (SecretProperties secretProperties in allSecrets)
                {
                    string secretName = secretProperties.Name;
                    string secretValue = Client.GetSecret(secretName).Value.ToString();
                    allSecretsList.Add(secretName, secretValue);
                }
            }
            catch (Exception)
            {
                allSecretsList = new Dictionary<string, string>();
            }
            return allSecretsList;
        }
    }
}
