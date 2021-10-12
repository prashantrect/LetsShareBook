using LSB.AzureAdapter.Models;

namespace LSB.AzureAdapter
{
    public class SqlDBManager : ISqlDBManager
    {
        private readonly AzureDBContext azureDBContext;
        private readonly string DBConnectionString;

        public SqlDBManager(string dbConnString)
        {
            this.DBConnectionString = dbConnString;
            azureDBContext = new AzureDBContext(DBConnectionString);
        }

       
    }
}
