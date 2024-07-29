using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Server.Communicator;
using Server.Data;
using Shared.Data;
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
            System.Console.WriteLine("--- Initializing Server. ---");

            
            if(!communicator.Init()){
                System.Console.WriteLine("--- E: Finalizing Program. ---");
                return;
            }

            communicator.Listen();
        }

        public void Notify(MessageType message, object data)
        {
            switch(message){
                case MessageType.Communicator:
                    var req = data as Request;
                    var recievedDataString = Formatter.RemoveDelimiter(req.Data,req.Count);
                    
                    ExampleData recievedObject = Formatter.Deserialize<ExampleData>(recievedDataString);
                    System.Console.WriteLine($"Client {req!.SessionId}, says: {recievedObject.Message}");
                    
                    //Do bussiness logic
                    
                    /*System.Console.WriteLine("Sending response ...");

                    byte[] response = Formatter.Serialize("Pong");
                    response = Formatter.AddDelimiter(response);

                    communicator.Write(new Response(messageData.SessionId, response));*/
                break;
            }
        }
    }
}