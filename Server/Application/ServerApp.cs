using Server.Communicator;
using Server.Data;
using Shared.Data;
using Shared.Helper;
using Shared.Interface;
using Shared.Middleware;

namespace Server.Application
{
    public class ServerApp : IMediator
    {
        readonly ServerCommunicator communicator;

        public ServerApp()
        {
            communicator = new ServerCommunicator(
                this
            );
        }

        public void Init()
        {
            Logger.Log(LoggerLevel.Info, "Initializing Server");

            if (!communicator.Init())
            {
                Logger.Log(LoggerLevel.Error, "Finalizing Program");
                return;
            }

            communicator.Listen();
        }

        public void Notify(MessageType message, object data)
        {
            switch (message)
            {
                case MessageType.Communicator:
                    var transaction = data as Transaction;
                    var m = transaction!.Data;

                    //Do bussiness logic
                    if (m.Command == 0)
                    {
                        Logger.Log(LoggerLevel.Info, "Example data recieved");
                        ExampleData exampleData = Formatter.Deserialize<ExampleData>(m.Data.ToString()!);
                        Logger.Log(LoggerLevel.Info, $"Client {transaction!.SessionId}, says: {exampleData.Message}");

                        System.Console.WriteLine("Sending response ...");
                        Logger.Log(LoggerLevel.Info, "Sending response");
                        communicator.Write(new Transaction(transaction.SessionId, new Message<object>(0, new ExampleData("Pong!"))));
                    }
                break;
            }
        }
    }
}