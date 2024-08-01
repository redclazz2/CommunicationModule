using CommunicationClient.Communicator;
using CommunicationClient.Interface;
using CommunicationShared.Data;
using CommunicationShared.Helper;
using CommunicationShared.Interface;
using CommunicationShared.Middleware;

namespace CommunicationClient.Application
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

            if (connected)
            {
                System.Console.WriteLine("--- I: Communication Stablished with server. ---");
                communicator.Write(new Message<object>(
                0,
                new ExampleData("Ping!")
            ));
                communicator.Read();
            }
            else
            {
                System.Console.WriteLine(" --- E: Failed to initialize connection to network. ---");
            }

            //Continue with app components here.
        }

        public void Notify(MessageType message, object data)
        {
            switch (message)
            {
                case MessageType.Communicator:
                    var m = data as Message<object>;

                    //Do bussiness logic
                    if (m.Command == 0)
                    {
                        Logger.Log(LoggerLevel.Info, "Example data recieved");
                        ExampleData exampleData = Formatter.Deserialize<ExampleData>(m.Data.ToString()!);
                        Logger.Log(LoggerLevel.Info, $"Server says: {exampleData.Message}");

                        this.communicator.Close();
                    }
                    break;
            }
        }
    }
}