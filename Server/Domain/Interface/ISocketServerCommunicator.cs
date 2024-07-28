using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Domain.Base;

namespace Server.Interface
{
    public interface ISocketServerCommunicator
    {
        public bool Init();
        public bool Close();
        public BaseSocketSessionCommunicator Listen();
    }
}