using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace QuestionsWereAsked.Common.Messages
{
    public class SecureMessage : MessageBase
    {

        public SecureMessage()
        {
        }

        public SecureMessage WithRsaParameters(RSAParameters param)
        {
            Q = param.Q;
            P = param.P;
            D = param.D;
            DP = param.DP;
            DQ = param.DQ;
            Exponent = param.Exponent;
            InverseQ = param.InverseQ;
            Modulus = param.Modulus;
            return this;
        }

        public RSAParameters ToRsaParameters()
        {
            return new RSAParameters
            {
                Q = Q,
                P = P,
                DQ = DQ,
                Modulus = Modulus,
                DP = DP,
                D = D,
                Exponent = Exponent,
                InverseQ = InverseQ
            };
        }

        public byte[] Q { get; set; }
        public byte[] P { get; set; }

        public byte[] DQ { get; set; }

        public byte[] Modulus { get; set; }

        public byte[] InverseQ { get; set; }

        public byte[] Exponent { get; set; }

        public byte[] DP { get; set; }

        public byte[] D { get; set; }
    }
}
