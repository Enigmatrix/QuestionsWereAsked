using System;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Threading;
using QuestionsWereAsked.Common;
using QuestionsWereAsked.Common.Messages;

namespace QuestionsWereAsked.Client.ViewModel
{
    public class GameService
    {
        private GameStateMessage lastStateMessage;
        private TripleDESCryptoServiceProvider symmetric;

        public GameService(string nick, string host)
        {
            Host = host;
            Nick = nick;
            ListeningThread = new Thread(Listener);
            ListeningThread.Start();
        }

        private Socket Socket { get; set; }
        private string Host { get; }
        public string Nick { get; }

        private Thread ListeningThread { get; }

        private void Listener()
        {
            var tcpClient = new TcpClient(Host, Config.Port);
            Socket = tcpClient.Client;
            var secureMessage = Socket.Receive<SecureMessage>();
            var rsa = Config.CreateRsa();
            rsa.ImportParameters(secureMessage.ToRsaParameters());

            symmetric = new TripleDESCryptoServiceProvider();
            symmetric.GenerateIV();
            symmetric.GenerateKey();

            Socket.SendRSA(new SecureReplyMessage(Nick, symmetric.Key, symmetric.IV), rsa);

            var res = true;
            while (res)
            {
                try
                {
                    lastStateMessage = Socket.ReceiveTripleDES<GameStateMessage>(symmetric);
                    if (GameStateChanged != null) GameStateChanged(lastStateMessage);
                }
                catch (SocketException se)
                {
                    res = false;
                }
            }
        }


        public event Action<GameStateMessage> GameStateChanged;

        public void SendAnswer(int i)
        {
            Socket.SendTripleDES(new AnswerMessage{ChosenIndex = i}, symmetric);
        }
    }
}