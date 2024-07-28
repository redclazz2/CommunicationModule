using System.Net.Sockets;
using Client.Interface;
using Shared.Interface;

namespace Client.Communicator
{
    public class ClientCommunicator : IClientCommunicator
    {
        IMediator mediator;
        ISocketClientCommunicator socket;

        public ClientCommunicator(IMediator mediator, int port, ProtocolType protocolType){
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

        public object Read()
        {
            return socket.Read().Result;
        }

        public void Write()
        {
            socket.Write("Pong");
        }
    }
}