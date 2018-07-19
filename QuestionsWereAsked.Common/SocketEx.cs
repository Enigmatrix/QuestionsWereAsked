using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using QuestionsWereAsked.Common;

namespace QuestionsWereAsked.Common
{
    public static class SocketEx
    {

        public static T Receive<T>(this Socket socket)
        {
            var buf = new byte[2048];
            socket.Receive(buf);
            var s = Config.Decode(buf);
            return JsonConvert.DeserializeObject<T>(s, Config.JsonSettings);
        }

        public static T ReceiveRSA<T>(this Socket socket, RSA rsa)
        {
            var buf = new byte[256];
            socket.Receive(buf);
            var decrypted = buf.Decrypt(rsa);
            var s = Config.Decode(decrypted);
            return JsonConvert.DeserializeObject<T>(s, Config.JsonSettings);
        }

        public static T ReceiveTripleDES<T>(this Socket socket, TripleDESCryptoServiceProvider triple)
        {
            var buf = new byte[2048];
            var sz = socket.Receive(buf);
            var decrypted = buf.Decrypt(triple, sz);
            var s = Config.Decode(decrypted);
            return JsonConvert.DeserializeObject<T>(s, Config.JsonSettings);
        }

        public static void SendRSA<T>(this Socket socket, T obj, RSA rsa)
        {
            var encryptedBuf = JsonConvert.SerializeObject(obj, Config.JsonSettings)
                .Encode()
                .Encrypt(rsa);
            socket.Send(encryptedBuf);
        }

        public static void SendTripleDES<T>(this Socket socket, T obj, TripleDESCryptoServiceProvider tripleDES)
        {
            var encryptedBuf = JsonConvert.SerializeObject(obj, Config.JsonSettings)
                .Encode()
                .Encrypt(tripleDES);
            socket.Send(encryptedBuf);
        }

        public static void Send<T>(this Socket socket, T obj)
        {
            var encryptedBuf = JsonConvert.SerializeObject(obj, Config.JsonSettings)
                .Encode();
            socket.Send(encryptedBuf);
        }
    }
}
