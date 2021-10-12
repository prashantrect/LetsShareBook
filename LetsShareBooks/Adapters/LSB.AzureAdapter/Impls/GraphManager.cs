using Microsoft.Graph;
using LSB.AzureAdapter.Helpers;
using System;
using System.Threading.Tasks;

namespace LSB.AzureAdapter
{
    public class GraphManager : IGraphManager
    {
        public GraphServiceClient graphClient { get; set; }

        public GraphManager()
        {
            graphClient = (new GraphHelper()).CreateGraphClient();
        }

        public async Task<User> GetCurrentUserInfo()
        {
            try
            {
                var user = await graphClient.Me
                    .Request()
                    .Select(u => new
                    {
                        u.DisplayName,
                        u.JobTitle
                    })
                    .GetAsync();

                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
