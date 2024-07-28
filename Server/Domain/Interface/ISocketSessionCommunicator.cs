using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Data;

namespace Server.Interface
{
    public interface ISocketSessionCommunicator
    {
        public Task<MessageData> Read();
        public void Write(MessageData data);
        public bool Close();
    }
}