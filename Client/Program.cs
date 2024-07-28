using Client.Application;

namespace Client;

class Program
{
    static void Main(string[] args)
    {
        ClientApp clientApp = new();
        clientApp.Init();
    }
}
