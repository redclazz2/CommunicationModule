using System.Net.Sockets;
using Server.Data;
using Server.Interface;

namespace Server.Communicator
{
    public class SocketSessionCommunicator
    {
        public int sessionId;
        public Socket socket;
        public SocketSessionCommunicator(Socket socket, int sessionId){
            this.sessionId = sessionId;
            this.socket = socket;
        }

        public  bool Close()
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

        public  async Task<Request> Read()
        {
            var buffer = new byte[1024];
            var received = await socket.ReceiveAsync(buffer, SocketFlags.None);
            
            return new Request(sessionId, buffer, received);
        }

        public  async void Write(Response data)
        {
            try
            {
                await socket.SendAsync(data.Data, 0);
            }
            catch (Exception e)
            {
                System.Console.WriteLine($"Error: {e}");
            }
        }
    }
}