using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace QuestionsWereAsked.Common
{
    public static class Config
    {
        public static readonly int Port = 6969;

        public static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            
        };

        public static byte[] Encode(this string s)
        {
            return Encoding.UTF8.GetBytes(s);
        }

        public static string Decode(byte[] buf)
        {
            return Encoding.UTF8.GetString(buf);
        }

        public static byte[] Decrypt(this byte[] buf, RSA rsa)
        {
            return rsa.Decrypt(buf, RSAEncryptionPadding.Pkcs1);
        }

        public static byte[] Decrypt(this byte[] buf, TripleDESCryptoServiceProvider triple, int sz)
        {
            var tdes = triple.CreateDecryptor();
            return tdes.TransformFinalBlock(buf, 0, sz);
        }

        public static byte[] Encrypt(this byte[] buf, RSA rsa)
        {
            return rsa.Encrypt(buf, RSAEncryptionPadding.Pkcs1);
        }

        public static byte[] Encrypt(this byte[] buf, TripleDESCryptoServiceProvider triple)
        {
            var tdes =  triple.CreateEncryptor();
            return tdes.TransformFinalBlock(buf, 0, buf.Length);
        }

        public static RSA CreateRsa()
        {
            var rsa = RSA.Create();
            return rsa;
        }
    }
}
