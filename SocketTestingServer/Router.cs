using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SocketTestingServer
{
    class Router
    {
        private List<Client> clients = new List<Client>();

        public void processMessage(String data, EndPoint endPoint)
        {
            Console.WriteLine("Received: " + data);
            data = data.Replace("<EOF>", "");
            data = data.Trim();

            if (data.ToLower().StartsWith("login"))
            {
                Console.WriteLine("New Login from: " + endPoint.ToString());
                clients.Add(new Client(data.Substring(5).Trim(), (IPEndPoint)endPoint));
                announce(data.Substring(5).Trim() + " has Connected!");
            }
            else if (data.ToLower().StartsWith("logout"))
            {
                foreach(Client client in clients){
                    if (client.Equals(endPoint))
                    {
                        announce(client.GetUsername() + " has Disconnected!");
                        break;
                    }
                }
                clients.Remove(new Client("", (IPEndPoint)endPoint));
            }
            else if (data.ToLower().StartsWith("tell"))
            {
                data = data.Substring(4).Trim();

                foreach(Client client in clients)
                {
                    if (client.Equals(endPoint))
                    {
                        announce(client.GetUsername() + ": " + data);
                        break;
                    }
                }
            }
        }

        public void announce(String data)
        {
            foreach(Client client in clients)
            {
                client.Send(data);
            }
        }

        
    }
}
