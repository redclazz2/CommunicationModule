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

        public ServerApp(){
            communicator = new ServerCommunicator(
                this
            );
        }

        public void Init(){
            Logger.Log(LoggerLevel.Info,"Initializing Server");

            if(!communicator.Init()){
                Logger.Log(LoggerLevel.Error,"Finalizing Program");
                return;
            }

            communicator.Listen();
        }

        public void Notify(MessageType message, object data)
        {
            switch(message){
                case MessageType.Communicator:
                    
                    //Security logic can be handled here.

                    var req = data as Request;
                    var recievedDataString = Formatter.RemoveDelimiter(req!.Data,req.Count);
                    Message<object> recievedObject = Formatter.Deserialize<Message<object>>(recievedDataString);
                    
                    //Do bussiness logic
                    if(recievedObject.Command == 0){
                        Logger.Log(LoggerLevel.Info,"Example data recieved");
                        ExampleData exampleData = Formatter.Deserialize<ExampleData>(recievedObject.Data.ToString()!);
                        Logger.Log(LoggerLevel.Info,$"Client {req!.SessionId}, says: {exampleData.Message}");
                    }
      
                    /*System.Console.WriteLine("Sending response ...");

                    byte[] response = Formatter.Serialize("Pong");
                    response = Formatter.AddDelimiter(response);

                    communicator.Write(new Response(messageData.SessionId, response));*/
                break;
            }
        }
    }
}