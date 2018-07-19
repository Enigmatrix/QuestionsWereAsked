using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using QuestionsWereAsked.Common;
using QuestionsWereAsked.Common.Messages;

namespace QuestionsWereAsked.Server
{
    public class GameManager
    {
        public GameManager()
        {
            Games = new List<Game>();
            WaitingPlayers = new ConcurrentQueue<Player>();
        }

        public List<Game> Games { get; }

        public ConcurrentQueue<Player> WaitingPlayers { get; set; }

        public void ConsolidatePlayersIntoGames()
        {
            if (WaitingPlayers.Count < 2)
                return;
            WaitingPlayers.TryDequeue(out var p1);
            WaitingPlayers.TryDequeue(out var p2);
            var game = new Game(p1, p2);
            Games.Add(game);
            game.End += GameEnded;
            game.Start();
        }

        public void GameEnded(Game game)
        {
            game.End -= GameEnded;
            Games.Remove(game);
            if(game.Player1.Socket.Connected)
                game.Player1.Socket.Close();
            if(game.Player2.Socket.Connected)
                game.Player2.Socket.Close();
        }

        public void IncludeNewPlayer(Socket socket)
        {
            var sendRsa = Config.CreateRsa();

            var export = sendRsa.ExportParameters(false);
            var secureMessage = new SecureMessage().WithRsaParameters(export);
            socket.Send(secureMessage);

            var reply = socket.ReceiveRSA<SecureReplyMessage>(sendRsa);

            var symmetric = new TripleDESCryptoServiceProvider
            {
                IV = reply.IV,
                Key = reply.Key
            };

            var player = new Player
            {
                Nick = reply.Nick,
                Socket = socket,
                Crypt = symmetric
            };
            WaitingPlayers.Enqueue(player);

            Console.WriteLine(player.Nick);

            ConsolidatePlayersIntoGames();
        }
    }
}
