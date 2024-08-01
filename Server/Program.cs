using Microsoft.Extensions.Configuration;
using CommunicationServer.Application;

namespace CommunicationServer;

class Program
{
    static void Main(string[] args)
    {
        ServerApp server = new();
        server.Init();
    }
}
