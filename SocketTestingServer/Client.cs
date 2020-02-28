using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketTestingServer {
    class Client : IEquatable<Client>
    {
        private IPEndPoint endPoint;
        private String username;

        public Client(String username, IPEndPoint endPoint)
        {
            this.username = username;
            this.endPoint = endPoint;
            this.endPoint.Port = 11111;
        }

        public IPEndPoint GetEndPoint()
        {
            return endPoint;
        }

        public String GetUsername()
        {
            return username;
        }

        public void Send(String data)
        {
            try
            {
                Console.WriteLine("Attempting to send to: ");
                Console.WriteLine(endPoint.ToString());


                Socket sender = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    // Connect Socket to the remote  
                    // endpoint using method Connect() 
                    sender.Connect(endPoint);

                    data += "<EOF>";

                    // Creation of messagge that 
                    // we will send to Server 
                    byte[] messageSent = Encoding.ASCII.GetBytes(data);
                    int byteSent = sender.Send(messageSent);

                    Console.WriteLine("Sent to " + username + ": " + data);

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

        public bool Equals(Client other)
        {
            if(endPoint.Address == other.endPoint.Address)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public bool Equals(EndPoint other)
        {
            if(endPoint.Address == ((IPEndPoint)other).Address)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}