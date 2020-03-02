using System;
using System.Collections.Generic;
using System.Text;

namespace SocketTestingServer
{
    class MessageHandler
    {
        ServerRoom serverRoom;

        public MessageHandler(ServerRoom server)
        {
            this.serverRoom = server;
        }

        public void Process(String message, ClientSocket sender)
        {
            //Console.WriteLine("Received: " + message);
            message = message.Replace("<EOF>", "");
            message = message.Trim();

            if (message.ToLower().StartsWith("login"))
            {
                //Console.WriteLine("New Login from: " + endPoint.ToString());
                sender.SetUsername(message.Substring(5).Trim());
                serverRoom.Connect(sender);
                serverRoom.AnnounceToAll(sender.GetUsername() + " has Connected!");
                Console.WriteLine(sender.GetUsername() + " has Connected!");
            }
            else if (message.ToLower().StartsWith("logout"))
            {
                serverRoom.AnnounceToAll(sender.GetUsername() + " has Disconnected!");
                serverRoom.Disconnect(sender);
                sender.Shutdown();
            }
            else if (message.ToLower().StartsWith("tell"))
            {
                message = message.Substring(4).Trim();
                serverRoom.AnnounceToAll(sender.GetUsername() + ": " + message);
                Console.WriteLine(sender.GetUsername() + ": " + message);
            }
        }
    }
}
