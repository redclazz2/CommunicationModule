using Microsoft.Extensions.Configuration;
using Server.Application;

namespace Server;

class Program
{
    static void Main(string[] args)
    {
        ServerApp server = new();
        server.Init();
    }
}
