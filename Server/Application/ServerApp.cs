using Server.Communicator;
using Server.Data;
using Server.Interface;
using Shared.Interface;

namespace Server.Application
{
    public class ServerApp : IMediator
    {
        IServerCommunicator communicator;

        public ServerApp(){
            communicator = new ServerCommunicator(
                this,
                8059,
                System.Net.Sockets.ProtocolType.Tcp
            );
        }

        public void Init(){
            communicator.Init();
        }

        public void Notify(MessageType message, object data)
        {
            switch(message){
                case MessageType.Communicator:
                    var messageData = data as MessageData;
                    System.Console.WriteLine($"Client: {messageData!.SessionId} says: {messageData!.Data}");
                    
                    //Do bussiness logic

                    communicator.Write(new MessageData(messageData.SessionId, "Pong"));
                break;
            }
        }
    }
}