using System;
using System.Threading.Tasks;

namespace SocketTestingClient
{
    class Program
    {
        static void Main(string[] args)
        {
            String input = "";
            Task listenerTask;

            while (!input.ToLower().StartsWith("logout"))
            {
                input = Console.ReadLine();

                if (input.ToLower().StartsWith("login"))
                {
                    listenerTask = Listener.Listen(input.Substring(5).Trim());
                } else if (input.ToLower().StartsWith("logout"))
                {
                    Client.Send(input);
                } else if (input.ToLower().StartsWith("tell"))
                {
                    Client.Send(input);
                } else if (input.ToLower().StartsWith("help"))
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
