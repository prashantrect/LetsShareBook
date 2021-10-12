using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSB.AzureAdapter
{
    public interface IGraphManager
    {
        Task<User> GetCurrentUserInfo();
    }
}
