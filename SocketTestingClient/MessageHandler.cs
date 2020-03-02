using System;
using System.Collections.Generic;
using System.Text;

namespace SocketTestingClient
{
    class MessageHandler
    {
        ClientSocket socket;
        public MessageHandler(ClientSocket socket)
        {
            this.socket = socket;
        }

        public void Process(String message)
        {
            //Console.WriteLine("Received: " + message);
            message = message.Replace("<EOF>", "");
            message = message.Trim();

            if (message.StartsWith("heartbeat"))
            {
                socket.Send("heartbeat");
            } else
            {
                Console.WriteLine(message);
            }
        }
    }
}
