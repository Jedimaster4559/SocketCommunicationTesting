using System;
using System.Threading.Tasks;

namespace SocketTestingClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientSocket clientSession = null;
            String input = "";

            while (!input.ToLower().StartsWith("logout"))
            {
                input = Console.ReadLine();

                if (input.ToLower().StartsWith("login"))
                {
                    clientSession = new ClientSocket(input.Substring(5).Trim());
                    Task task = Task.Run(() => clientSession.Listen());
                }
                else if (input.ToLower().StartsWith("logout"))
                {
                    clientSession.Send(input);
                    clientSession.Shutdown();
                }
                else if (input.ToLower().StartsWith("tell"))
                {
                    clientSession.Send(input);
                }
                else if (input.ToLower().StartsWith("help"))
                {
                    Console.WriteLine("Welcome to my Client-Server Testing program");
                    Console.WriteLine("This program has been developed by Nathan Solomon to test and work with Client-Server Architectures and their implementations\n");
                    Console.WriteLine("Available Commands:");
                    Console.WriteLine("login [username] ---> Logs a user in");
                    Console.WriteLine("logout ---> Logs a user out");
                    Console.WriteLine("tell ---> Sends a message to all users in the chat room");
                }

            }
        }
    }
}
