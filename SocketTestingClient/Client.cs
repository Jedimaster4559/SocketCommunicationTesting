using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketTestingClient
{
    class Client
    {

        public static void Send(String data)
        {
            try
            {
                IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddr = ipHost.AddressList[0];
                IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

                Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    // Connect Socket to the remote  
                    // endpoint using method Connect() 
                    sender.Connect(localEndPoint);

                    data += "<EOF>";

                    // Creation of messagge that 
                    // we will send to Server 
                    byte[] messageSent = Encoding.ASCII.GetBytes(data);
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
       
