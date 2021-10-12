using LSB.Models;
using System;

namespace LSB.AzureAdapter
{
    public class CosmosManager: ICosmosManager
    {
        //private readonly AzureDBContext azureDBContext;
        private readonly string DBConnectionString;

        public CosmosManager(string connString)
        {
            //this.DBConnectionString = dbConnString;
            //azureDBContext = new AzureDBContext(DBConnectionString);
        }

        public User GetUser(string userId)
        {
            throw new NotImplementedException();
        }

        public User CreateUser()
        {
            throw new NotImplementedException();
        }
    }
}
