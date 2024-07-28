using System.Net;
using System.Net.Sockets;
using System.Text;
using Client.Application;

namespace Client;

class Program
{
    static void Main(string[] args)
    {
        ClientApp clientApp = new();
        clientApp.Init();

        
        /*while (true)
        {
            // Send message.
            

            // Receive ack.
            
            // Sample output:
            //     Socket client sent message: "Hi friends 👋!<|EOM|>"
            //     Socket client received acknowledgment: "<|ACK|>"
        }*/

        
    }
}
