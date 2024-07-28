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
        public override async Task<MessageData> Read()
        {
            var buffer = new byte[1024];
            var received = await socket.ReceiveAsync(buffer, SocketFlags.None);
            var response = Encoding.UTF8.GetString(buffer, 0, received);
            var data = "";

            var eom = "<|EOM|>";
            if (response.IndexOf(eom) > -1 /* is end of message */)
            {
                data = response.Replace(eom, "");
            }

            return new MessageData(sessionId, data);
        }

        //Modify write procedures here.
        public override async void Write(MessageData data)
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