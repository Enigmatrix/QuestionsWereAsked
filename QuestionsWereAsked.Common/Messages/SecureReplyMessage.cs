using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace QuestionsWereAsked.Common.Messages
{
    public class SecureReplyMessage : MessageBase
    {
        public string Nick { get; set; }

        public byte[] Key { get; set; }
        public byte[] IV { get; set; }

        public SecureReplyMessage(string nick,byte[] key, byte[] iv)
        {
            Nick = nick;
            Key = key;
            IV = iv;
        }

    }
}
