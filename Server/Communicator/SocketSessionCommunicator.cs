using System.Net.Sockets;
using System.Text;
using Server.Data;
using Server.Domain.Base;

namespace Server.Communicator
{
    public class SocketSessionCommunicator : BaseSocketSessionCommunicator
    {
        public SocketSessionCommunicator(Socket socket, int sessionId):base(socket,sessionId){}

        public override bool Close()
        {
            try
            {
                socket.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Modify the read procedures here.
        public override async Task<Request> Read()
        {
            var buffer = new byte[1024];
            var received = await socket.ReceiveAsync(buffer, SocketFlags.None);
            
            return new Request(sessionId, buffer, received);
        }

        //Modify write procedures here.
        public override async void Write(Response data)
        {
            try
            {
                var ackMessage = $"{data.Data}<|ACK|>";
                var echoBytes = Encoding.UTF8.GetBytes(ackMessage);
                await socket.SendAsync(echoBytes, 0);
            }
            catch (Exception e)
            {
                System.Console.WriteLine($"Error: {e}");
            }
        }
    }
}