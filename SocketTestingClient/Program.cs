using System;
using System.Threading.Tasks;

namespace SocketTestingClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Task task = Listener.Listen();
            Client.Execute();
        }
    }
}
