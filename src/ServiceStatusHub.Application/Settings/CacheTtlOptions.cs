using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStatusHub.Application.Settings
{
    public class CacheTtlOptions
    {
        public Dictionary<string, int> Expirations { get; set; } = new();
    }
}
