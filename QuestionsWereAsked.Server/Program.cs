using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using QuestionsWereAsked.Common;

namespace QuestionsWereAsked.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var gameManager = new GameManager();
            var listener = new TcpListener(IPAddress.Any, Config.Port);
            listener.Start();
            Console.WriteLine("Listening to connections...");

            while (true)
            {
                var socket = listener.AcceptSocket();
                gameManager.IncludeNewPlayer(socket);
            }

        }
    }
}
