using System.Net.Sockets;
using Shared.Data;

namespace Server.Communicator
{
    public class SocketSession
    {
        public int sessionId;
        public Socket socket;
        public SocketSession(Socket socket, int sessionId){
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

        public  async Task<ReadData> Read()
        {
            var buffer = new byte[1024];
            var recieved = await socket.ReceiveAsync(buffer, SocketFlags.None);
            
            return new ReadData(buffer, recieved);
        }

        public  async void Write(byte[] data)
        {
            try
            {
                await socket.SendAsync(data, 0);
            }
            catch (Exception e)
            {
                System.Console.WriteLine($"Error: {e}");
            }
        }
    }
}