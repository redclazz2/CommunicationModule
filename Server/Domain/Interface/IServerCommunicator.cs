using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Data;

namespace Server.Interface
{
    public interface IServerCommunicator
    {
        public void Init();
        public void Read(ISocketSessionCommunicator session);
        public bool Write(MessageData data);
    }
}