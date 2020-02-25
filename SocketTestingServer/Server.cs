using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketTestingServer {
    class Server {
        public static void Execute() {
            // Setup the local endpoint of the server
            // I wonder how adaptable this is? We will see later?
            IPHostEntry ipHost = Dns.GetHostEntry("cloud.gameserver-us001.hypernovastudios.com"); 
            IPAddress ipAddr = ipHost.AddressList[0]; 
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

            Socket listener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            Console.WriteLine("DNS: " + Dns.GetHostName());
            Console.WriteLine("IPHost: " + ipHost.ToString());
            Console.WriteLine("IPAddr: " + ipAddr.ToString());
            Console.WriteLine("EndPoint: " + localEndPoint.ToString());

            try {
                listener.Bind(localEndPoint);

                listener.Listen(10);

                while(true){
                    Console.WriteLine("Waiting for connections...");

                    Socket client = listener.Accept();

                    // Data buffer 
                    byte[] bytes = new Byte[1024]; 
                    string data = null; 
        
                    while (true) { 
        
                        int numByte = client.Receive(bytes); 
                        
                        data += Encoding.ASCII.GetString(bytes, 
                                                0, numByte); 
                                                    
                        if (data.IndexOf("<EOF>") > -1) 
                            break; 
                    } 
        
                    Console.WriteLine("Text received -> {0} ", data); 
                    byte[] message = Encoding.ASCII.GetBytes("Received!"); 
        
                    // Send a message to Client  
                    // using Send() method 
                    client.Send(message); 
        
                    // Close client Socket using the 
                    // Close() method. After closing, 
                    // we can use the closed Socket  
                    // for a new Client Connection 
                    client.Shutdown(SocketShutdown.Both); 
                    client.Close();
                }
            } catch (Exception e){
                Console.WriteLine(e.ToString());
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}