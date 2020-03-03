using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketTestingClient
{
    class ClientSocket
    {
        IPHostEntry targetIpHost;
        IPAddress targetIpAddr;
        IPEndPoint targetEndPoint;
        Socket socket;
        String username;

        public ClientSocket(String username)
        {
            targetIpHost = Dns.GetHostEntry("cloud.gameserver-us001.hypernovastudios.com");
            //targetiphost = dns.gethostentry(dns.gethostname());
            targetIpAddr = targetIpHost.AddressList[0];
            targetEndPoint = new IPEndPoint(targetIpAddr, 11111);
            socket = new Socket(targetIpAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            this.username = username;
        }

        public void Listen()
        {

            try
            {
                // Connect Socket to the remote  
                // endpoint using method Connect() 
                socket.Connect(targetEndPoint);

                // We print EndPoint information  
                // that we are connected 
                Console.WriteLine("Socket connected to -> {0} ",
                                socket.RemoteEndPoint.ToString());

                // Creation of message that 
                // we will send to Server 
                byte[] messageSent = Encoding.ASCII.GetBytes("Login " + username + "<EOF>");
                int byteSent = socket.Send(messageSent);

                MessageHandler handler = new MessageHandler(this);

                while (socket.Connected)
                {

                    // Data buffer 
                    byte[] messageReceived = new byte[1024];

                    // We receive the messagge using  
                    // the method Receive(). This  
                    // method returns number of bytes 
                    // received, that we'll use to  
                    // convert them to string 
                    int byteRecv = socket.Receive(messageReceived);
                    String message = Encoding.ASCII.GetString(messageReceived, 0, byteRecv);

                    handler.Process(message);


                }


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

        public void Send(String message)
        { 
            byte[] messageSent = Encoding.ASCII.GetBytes(message + "<EOF>");
            int byteSent = socket.Send(messageSent);
        }

        public void Shutdown()
        {
            // Close Socket using  
            // the method Close() 
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
}
       
