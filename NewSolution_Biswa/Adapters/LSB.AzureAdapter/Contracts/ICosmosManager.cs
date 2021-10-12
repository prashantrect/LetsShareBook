using LSB.Models;

namespace LSB.AzureAdapter
{
    public interface ICosmosManager
    {
        User GetUser(string userId);

        User CreateUser();
    }
}
