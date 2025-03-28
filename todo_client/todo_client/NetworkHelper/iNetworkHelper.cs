using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todo_client;

public interface iNetworkHelper
{
    bool HasInternet();
    Task<bool> IsHostReachable();
}