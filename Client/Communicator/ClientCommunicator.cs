using System.Net.Sockets;
using CommunicationClient.Interface;
using CommunicationShared.Data;
using CommunicationShared.Interface;
using CommunicationShared.Middleware;

namespace CommunicationClient.Communicator
{
    public class ClientCommunicator : IClientCommunicator
    {
        IMediator mediator;
        SocketClientCommunicator socket;

        public ClientCommunicator(IMediator mediator, int port, ProtocolType protocolType)
        {
            this.mediator = mediator;
            socket = new SocketClientCommunicator(
                port,
                protocolType
            );
        }

        public void Close()
        {
            socket.Close();
        }

        public bool Connect()
        {
            return socket.Connect().Result;
        }

        public void Read()
        {
            var data = socket.Read().Result;

            if (data != null)
            {
                mediator.Notify(MessageType.Communicator,
                    Formatter.Deserialize<Message<object>>(data.Data, data.Count)
                );
            }

        }

        public void Write(Message<object> message)
        {
            socket.Write(Formatter.Serialize(message));
        }
    }
}