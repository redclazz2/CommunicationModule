using CommunicationClient.Application;

namespace CommunicationClient;

class Program
{
    static void Main(string[] args)
    {
        ClientApp clientApp = new();
        clientApp.Init();
    }
}
