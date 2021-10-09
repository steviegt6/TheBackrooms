using System.Net;
using System.Net.Sockets;
using Terraria;

namespace TheBackrooms.Core.Utilities
{
    public static class ServerHelper
    {
        public static int GetFreeTcpPort(IPAddress address)
        {
            // Port of 0 indicates the next available port should be allocated.
            // Terraria uses a default port of 7777. This is arbitrary.
            TcpListener portFinder = new TcpListener(address, 0);
            portFinder.Start();
            int port = ((IPEndPoint) portFinder.LocalEndpoint).Port;
            portFinder.Stop();
            return port;
        }
    }
}