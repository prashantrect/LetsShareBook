using Microsoft.EntityFrameworkCore;

namespace LSB.AzureAdapter.Models
{
    public class AzureDBContext: DbContext
    {
        public string DBConnectionString { get; private set; }

        public AzureDBContext(string dbConnectionString)
        {
            DBConnectionString = dbConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer(DBConnectionString);
    }
}
