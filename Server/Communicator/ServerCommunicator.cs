using Server.Interface;
using Server.Data;
using Shared.Interface;

namespace Server.Communicator
{
    public class ServerCommunicator : IServerCommunicator
    {
        private readonly IMediator mediator;
        private readonly ISocketServerCommunicator serverSocket;
        private readonly Dictionary<int, ISocketSessionCommunicator> clients = [];

        public ServerCommunicator(IMediator mediator)
        {
            this.mediator = mediator;
            //TODO: Read Port and Protocol from configuration file
            serverSocket = new SocketServerCommunicator(
                8056,
                System.Net.Sockets.ProtocolType.Tcp
            );
        }

        public bool Init()
        {
            System.Console.WriteLine("--- I: Initializing Communication Module. ---");
            var init = serverSocket.Init();

            if(init){
                System.Console.WriteLine("--- I: Success. ---");
            }else{
                System.Console.WriteLine("--- E: Failed to Initialize Communication Module. ---");
            }

            return  init;
        }

        public void Close()
        {
            System.Console.WriteLine("Closing server socket ...");
            if (serverSocket.Close())
            {
                foreach (SocketSessionCommunicator c in clients.Values.Cast<SocketSessionCommunicator>()) c.Close();
                clients.Clear();
                System.Console.WriteLine("Bye!");
            }
            else
            {
                System.Console.WriteLine("Error on server communication module clean up.");
            }
        }

        public void Listen()
        {
            while (true)
            {
                System.Console.WriteLine($"Listening ...");
                
                //In the future a separated thread can be instantiated here.

                var session = serverSocket.Listen();
                clients.Add(session.sessionId, session);
                Read(session.sessionId);
            }
        }

        public void Read(int sessionId)
        {
            var session = clients.GetValueOrDefault(sessionId);
            var data = session?.Read().Result;
            
            if(data != null){
                mediator.Notify(MessageType.Communicator, data);
            }      
        }

        public void Write(Response data)
        {
            var session = clients.GetValueOrDefault(data.SessionId);
            session?.Write(data);
        }
    }
}