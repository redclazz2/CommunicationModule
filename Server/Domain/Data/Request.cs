namespace Server.Data
{
    public class Request(int sessionId, byte[] data, int count)
    {
        private readonly int sessionId = sessionId;

        public int SessionId{
            get{ return sessionId;}
        }

        private readonly byte[] data = data;

        public byte[] Data{
            get { return data;}
        }

        private readonly int count = count;

        public int Count{
            get {return count;}
        }
    }
}