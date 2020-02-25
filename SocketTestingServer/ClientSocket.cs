using System;
using System.Net;
using System.Net.Sockets;

namespace SocketTestingServer {
    class ClientSocket : Socket
    {
        public ClientSocket(SocketInformation socketInformation) : base(socketInformation)
        {
        }

        public ClientSocket(SocketType socketType, ProtocolType protocolType) : base(socketType, protocolType)
        {
        }

        public ClientSocket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType) : base(addressFamily, socketType, protocolType)
        {
        }
    }
}