using Shared.Data;

namespace Server.Data
{
    public class Transaction(int sessionId, Message<object> data)
    {
        private readonly int sessionId = sessionId;

        public int SessionId{
            get{ return sessionId;}
        }

        private readonly Message<object>  data = data;

        public Message<object>  Data{
            get { return data;}
        }
    }
}