using Client.Communicator;
using Client.Interface;
using Shared.Interface;

namespace Client.Application
{
    public class ClientApp : IMediator
    {
        IClientCommunicator communicator;

        public ClientApp()
        {
            communicator = new ClientCommunicator(
                this,
                8056,
                System.Net.Sockets.ProtocolType.Tcp
            );
        }

        public void Init()
        {
            System.Console.WriteLine("--- Initializing Client App ---");

            System.Console.WriteLine("--- I: Initializing Communication Module ---");

            var connected = communicator.Connect(); 

            if(connected){
                System.Console.WriteLine("--- I: Communication Stablished with server. ---");
                communicator.Write();
                communicator.Read();
            }else{
                System.Console.WriteLine(" --- E: Failed to initialize connection to network. ---");
            }

            //Continue with app components here.
        }

        public void Notify(MessageType message, object data)
        {
            switch (message)
            {
                case MessageType.Communicator:
                    
                    break;
            }
        }
    }
}