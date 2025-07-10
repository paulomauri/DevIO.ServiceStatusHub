using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStatusHub.Application.Interfaces
{
    public interface ICachePolicyService
    {
        TimeSpan GetExpirationFor(string key);
    }
}
