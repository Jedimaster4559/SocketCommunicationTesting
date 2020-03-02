using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketTestingServer {
    class ServerSocket {

        // Setup the local endpoint of the server
        // I wonder how adaptable this is? We will see later?
        IPHostEntry localIpHost;
        IPAddress localIpAddr;
        IPEndPoint localEndPoint;
        Socket socket;
        String username;
        MessageHandler handler;

        public ServerSocket(MessageHandler handler)
        {
            // Setup the local endpoint of the server
            // I wonder how adaptable this is? We will see later?
            localIpHost = Dns.GetHostEntry("cloud.gameserver-us001.hypernovastudios.com");
            //localIpHost = Dns.GetHostEntry(Dns.GetHostName());
            localIpAddr = localIpHost.AddressList[0];
            localEndPoint = new IPEndPoint(localIpAddr, 11111);
            socket = new Socket(localIpAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            username = "";
            this.handler = handler;
        }
        public void Listen() {

            //Console.WriteLine("DNS: " + Dns.GetHostName());
            //Console.WriteLine("IPHost: " + ipHost.ToString());
            //Console.WriteLine("IPAddr: " + ipAddr.ToString());
            //Console.WriteLine("EndPoint: " + localEndPoint.ToString());

            try {
                socket.Bind(localEndPoint);

                socket.Listen(10);

                Console.WriteLine("Waiting for connections...");

                while (true)
                {
                    new ClientSocket(socket.Accept(), handler);
                }
                

            } catch (Exception e){
                Console.WriteLine(e.ToString());
                Console.WriteLine(e.StackTrace);
            }
        }

        
    }
}