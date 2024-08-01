using System.Net;
using System.Net.Sockets;
using Shared.Data;

namespace Client.Communicator
{
    public class SocketClientCommunicator
    {
        Socket socket;

        IPEndPoint ipEndPoint;

        public SocketClientCommunicator(int port, ProtocolType protocolType)
        {
            var hostName = Dns.GetHostName();
            IPHostEntry localhost = Dns.GetHostEntry(hostName);
            IPAddress localIpAddress = localhost.AddressList[0];
            ipEndPoint = new(localIpAddress, port);

            socket = new(
                ipEndPoint.AddressFamily,
                SocketType.Stream,
                protocolType
            );
        }

        public async Task<bool> Connect()
        {
            try{
                await socket.ConnectAsync(ipEndPoint);
                return true;
            }catch{
                return false;
            }
        }

        public bool Close()
        {
            try{
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                return true;
            }catch{
                return false;
            }
        }

        public async Task<ReadData> Read()
        {
            var buffer = new byte[1_024];
            var recieved = await socket.ReceiveAsync(buffer, SocketFlags.None);

            return new ReadData(buffer, recieved);
        }

        public async void Write(byte[] data)
        {
            await socket.SendAsync(data, SocketFlags.None);
        }
    }
}