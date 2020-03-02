using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocketTestingServer
{
    class ServerRoom
    {
        List<ClientSocket> clients;
        ServerSocket connectionListener; //used to wait for incoming connections
        MessageHandler handler;

        public ServerRoom()
        {
            clients = new List<ClientSocket>();

        }

        public async Task Start()
        {
            Console.WriteLine("Starting Server...");
            handler = new MessageHandler(this);
            connectionListener = new ServerSocket(handler);
            await Task.Run(() => connectionListener.Listen());
        }

        public void Connect(ClientSocket newClient)
        {
            clients.Add(newClient);
        }

        public void Disconnect(ClientSocket client)
        {
            clients.Remove(client);
        }

        public void AnnounceToAll(String message)
        {
            foreach(ClientSocket client in clients){
                client.Send(message);
            }
        }
    }
}
