using System.Net.Sockets;
using Server.Data;
using Server.Interface;
using Shared.Interface;

namespace Server.Communicator
{
    public class ServerCommunicator : IServerCommunicator
    {
        private readonly IMediator mediator;
        private readonly ISocketServerCommunicator serverSocket;
        private readonly Dictionary<int, ISocketSessionCommunicator> clients = [];

        public ServerCommunicator(IMediator mediator, int port, ProtocolType protocolType)
        {
            this.mediator = mediator;

            serverSocket = new SocketServerCommunicator(
                port,
                protocolType
            );
        }

        public bool Init()
        {
            return serverSocket.Init();
        }

        public void Close()
        {
            System.Console.WriteLine("Closing server socket ...");
            if(serverSocket.Close()){
                foreach(SocketSessionCommunicator c in clients.Values.Cast<SocketSessionCommunicator>()) c.Close();
                clients.Clear();
                System.Console.WriteLine("Bye!");
            }else{
                System.Console.WriteLine("Error on server communication module clean up.");
            }
        }

        public void Listen()
        {
            System.Console.WriteLine($"Listening ...");
            //In the future a separated thread can be instantiated here.

            var session = serverSocket.Listen();
            clients.Add(session.sessionId, session);
            Read(session);
        }

        public void Read(ISocketSessionCommunicator session)
        {
            var data = session.Read().Result;
            mediator.Notify(MessageType.Communicator, data);
        }

        public bool Write(MessageData data)
        {
            try
            {
                var session = clients.GetValueOrDefault(data.SessionId);
                session?.Write(data);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}