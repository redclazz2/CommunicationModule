using System.Net;
using System.Net.Sockets;
using System.Text;
using Client.Interface;
using Shared.Data;
using Shared.Middleware;

namespace Client.Communicator
{
    public class SocketClientCommunicator : ISocketClientCommunicator
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

        public async Task<object> Read()
        {
            var buffer = new byte[1_024];
            var received = await socket.ReceiveAsync(buffer, SocketFlags.None);
            var response = Encoding.UTF8.GetString(buffer, 0, received);
            System.Console.WriteLine(response);
            if (response.Contains("<|ACK|>"))  
            {
                Console.WriteLine(
                    $"Socket client received acknowledgment: \"{response}\"");
            }

            return response;
        }

        public async void Write(object data)
        {
            var message = new ExampleData("Ping!");
            var messageBytes = Formatter.Serialize(message);
            messageBytes = Formatter.AddDelimiter(messageBytes);

            await socket.SendAsync(messageBytes, SocketFlags.None);
            Console.WriteLine($"Socket client sent message: \"{messageBytes.Length}\"");
        }
    }
}