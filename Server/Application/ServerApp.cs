using System.Collections.Concurrent;
using Server.Communicator;
using Server.Data;
using Server.Interface;
using Shared.Interface;

namespace Server.Application
{
    public class ServerApp : IMediator
    {
        readonly ServerCommunicator communicator;

        public ServerApp(){
            communicator = new ServerCommunicator(
                this,
                8059,
                System.Net.Sockets.ProtocolType.Tcp
            );
        }

        public void Init(){
            System.Console.WriteLine("--- Initializing Server. ---");

            System.Console.WriteLine("--- I: Initializing Communication Module. ---");
            if(communicator.Init()){
                System.Console.WriteLine("--- I: Success. ---");
            }else{
                System.Console.WriteLine("--- E: Failed to Initialize Communication Module .---");
            }

            communicator.Listen();
        }

        public void Notify(MessageType message, object data)
        {
            switch(message){
                case MessageType.Communicator:
                    var messageData = data as MessageData;
                    System.Console.WriteLine($"Client: {messageData!.SessionId} says: {messageData!.Data}");
                    
                    //Do bussiness logic
                    
                    System.Console.WriteLine("Sending response ...");
                    communicator.Write(new MessageData(messageData.SessionId, "Pong"));

                    System.Console.WriteLine("Closing server ...");
                    communicator.Close();
                break;
            }
        }
    }
}