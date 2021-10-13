using System.Collections.Generic;
using System.Threading.Tasks;

namespace LSB.AzureAdapter
{
    public interface IKeyvaultManager
    {
        string FetchKeyvaultSecret(string secretName);

        string SetKeyvaultSecret(string secretName, string secretValue);

        string UpdateKeyvaultSecret(string secretName, string secretValue);

        string DeleteOrPurgeKeyvaultSecret(string secretName, bool isPurge);

        Dictionary<string, string> FetchKeyvaultSecretsList();

        Task<Dictionary<string, string>> FetchKeyvaultSecretsListAsync();
    }
}
