using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace SocketTestingClient
{
    class Listener
    {
        public static void Listen(String username)
        {
            //Let The Server know we are listening for things.
            announceListening(username);

            // Setup the local endpoint of the server
            // I wonder how adaptable this is? We will see later?
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = null;

            foreach(IPAddress addr in ipHost.AddressList){
                if(addr.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddr = addr;
                    break;
                }
            }

            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

            Socket listener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            Console.WriteLine("DNS: " + Dns.GetHostName());
            Console.WriteLine("IPHost: " + ipHost.ToString());

            Console.WriteLine("All Addresses:");
            foreach(IPAddress addr in ipHost.AddressList)
            {
                Console.WriteLine(addr.ToString());
            }

            Console.WriteLine("IPAddr: " + ipAddr.ToString());
            Console.WriteLine("EndPoint: " + localEndPoint.ToString());

            try
            {

                listener.Bind(localEndPoint);

                listener.Listen(10);

                while (true)
                {
                    Socket client = listener.Accept();

                    // Data buffer 
                    byte[] bytes = new Byte[1024];
                    string data = null;

                    while (true)
                    {

                        int numByte = client.Receive(bytes);

                        data += Encoding.ASCII.GetString(bytes,
                                                0, numByte);

                        if (data.IndexOf("<EOF>") > -1)
                            break;
                    }

                    // Close client Socket using the 
                    // Close() method. After closing, 
                    // we can use the closed Socket  
                    // for a new Client Connection 
                    client.Shutdown(SocketShutdown.Both);
                    client.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine(e.StackTrace);
            }
        }

        public static void announceListening(String username)
        {
            try
            {
                IPHostEntry ipHost = Dns.GetHostEntry("cloud.gameserver-us001.hypernovastudios.com");
                IPAddress ipAddr = ipHost.AddressList[0];
                IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

                Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    // Connect Socket to the remote  
                    // endpoint using method Connect() 
                    sender.Connect(localEndPoint);

                    // We print EndPoint information  
                    // that we are connected 
                    Console.WriteLine("Logged into: " +
                                  sender.RemoteEndPoint.ToString());

                    // Creation of messagge that 
                    // we will send to Server 
                    byte[] messageSent = Encoding.ASCII.GetBytes("login " + username + "<EOF>");
                    int byteSent = sender.Send(messageSent);

                  

                    // Close Socket using  
                    // the method Close() 
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                }

                // Manage of Socket's Exceptions 
                catch (ArgumentNullException ane)
                {

                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }

                catch (SocketException se)
                {

                    Console.WriteLine("SocketException : {0}", se.ToString());
                }

                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
