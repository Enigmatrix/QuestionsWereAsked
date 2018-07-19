using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace QuestionsWereAsked.Server
{
    public class Player
    {
        public string Nick { get; set; }
        public Socket Socket { get; set; }
        public TripleDESCryptoServiceProvider Crypt { get; set; }
    }
}
