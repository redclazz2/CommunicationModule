namespace Server.Data
{
    public class Response(int sessionId, byte[] data)
    {
        private readonly int sessionId = sessionId;

        public int SessionId{
            get{ return sessionId;}
        }

        private readonly byte[] data = data;

        public byte[] Data{
            get { return data;}
        }
    }
}