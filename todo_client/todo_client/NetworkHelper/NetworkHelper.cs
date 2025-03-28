using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Connectivity;

namespace todo_client;

public class NetworkHelper
{
    public static NetworkHelper network;
    public static NetworkHelper GetInstance
    {
        get
        {
            if (network == null)
            {
                network = new NetworkHelper();
            }
            return network;
        }
    }

    public bool HasInternet()
    {
        if (!CrossConnectivity.IsSupported)
        {
            return true;
        }
        return CrossConnectivity.Current.IsConnected;
    }

    public async Task<bool> IsHostReachable()
    {
        if (!CrossConnectivity.Current.IsConnected)
        {
            return false;
        }
        return true;
    }
}