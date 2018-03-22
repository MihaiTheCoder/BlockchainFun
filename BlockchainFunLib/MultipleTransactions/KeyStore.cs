using System;
using System.Collections.Generic;
using System.Text;

namespace BlockchainFunLib.MultipleTransactions
{
    public class KeyStore : IKeyStore
    {
        private FunCertificate Certificate;
        public byte[] AuthenticatedHashKey { get; private set; }

        public KeyStore(byte[] authenticatedhashKey)
        {
            AuthenticatedHashKey = authenticatedhashKey;
            Certificate = HashFun.AssignNewKey();
        }

        public string SignBlock(string blockHash)
        {
            return HashFun.SignData(blockHash, Certificate.PrivateKey);
        }

        public bool VerifyBlock(string blockHash, string signature)
        {
            return HashFun.VerifySignature(blockHash, signature, Certificate.PublicKey);
        }
    }
}
