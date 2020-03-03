using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace SocketTestingServer
{
    class ClientSocket
    {
        Socket socket;
        String username;
        MessageHandler handler;

        public ClientSocket(Socket socket, MessageHandler handler)
        {
            this.socket = socket;
            username = "";
            this.handler = handler;
        }

        public void Listen()
        {
            while (socket.Connected)
            {

                // Data buffer 
                byte[] bytes = new Byte[1024];
                string data = null;

                while (true)
                {

                    int numByte = socket.Receive(bytes);

                    data += Encoding.ASCII.GetString(bytes,
                                            0, numByte);

                    if (data.IndexOf("<EOF>") > -1)
                        break;
                }

                handler.Process(data, this);

            }

        }

        public void Send(String message)
        {

            byte[] data = Encoding.ASCII.GetBytes(message + "<EOF>");

            // Send a message to Client  
            // using Send() method 
            socket.Send(data);
        }

        public void Shutdown()
        {
            // Close client Socket using the 
            // Close() method. After closing, 
            // we can use the closed Socket  
            // for a new Client Connection 
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }

        public void SetUsername(String name)
        {
            username = name;
        }

        public string GetUsername()
        {
            return username;
        }
    }
}
