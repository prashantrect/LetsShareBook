using LSB.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSB.RestAdapter
{
    public class LSBServiceAdapter : RestClient, ILSBServiceAdapter
    {
        public LSBServiceAdapter(string name, string baseAddress, string resource, Func<string, Task<string>> tokenFunc)
            : base(name, baseAddress, resource, tokenFunc)
        {
        }
    }
}
