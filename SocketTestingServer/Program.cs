using System;

namespace SocketTestingServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerRoom server = new ServerRoom();
            server.Start().GetAwaiter().GetResult();
        }
    }
}
