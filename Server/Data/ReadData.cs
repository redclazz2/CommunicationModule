namespace Server.Data
{
    public class MessageData
    {
        private readonly int sessionId;

        public int SessionId{
            get{ return sessionId;}
        }

        private readonly string data;

        public string Data{
            get { return data;}
        }

        public MessageData(int sessionId, string data){
            this.sessionId = sessionId;
            this.data = data;
        }
    }
}