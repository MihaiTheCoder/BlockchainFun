using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BlockchainFunLib
{
    public static class HashFun
    {
        private const int KeySize = 32;

        public static byte[] Generate256BitKey()
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[KeySize];
                randomNumberGenerator.GetBytes(randomNumber);

                return randomNumber;
            }
        }

        public static string GetSha256(string toBeHashed)
        {
            // SHA512 is disposable by inheritance.  
            using (var sha256 = SHA256.Create())
            {
                // Send a sample text to hash.  
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(toBeHashed));
                // Get the hashed string.  
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public static string GetHMACHash(string toBeHashed, string key)
        {
            var keyToUse = Encoding.UTF8.GetBytes(key);
            var message = Encoding.UTF8.GetBytes(toBeHashed);

            using (var hmac = new HMACSHA256(keyToUse))
            {
                return Convert.ToBase64String(hmac.ComputeHash(message));
            }
        }

        public static string GetHMACHash(string toBeHashed, byte[] key)
        {
            var message = Encoding.UTF8.GetBytes(toBeHashed);

            using (var hmac = new HMACSHA256(key))
            {
                return Convert.ToBase64String(hmac.ComputeHash(message));
            }
        }

        public static FunCertificate AssignNewKey()
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;

                var publicKey = rsa.ExportParameters(false);
                var privateKey = rsa.ExportParameters(true);
                return new FunCertificate(publicKey, privateKey);
            }
        }

        public static string SignData(string hashOfDataToSign, RSAParameters privateKey)
        {
            return Convert.ToBase64String(SignData(Convert.FromBase64String(hashOfDataToSign), privateKey));
        }


        public static byte[] SignData(byte[] hashOfDataToSign, RSAParameters privateKey)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(privateKey);

                var rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
                rsaFormatter.SetHashAlgorithm("SHA256");

                return rsaFormatter.CreateSignature(hashOfDataToSign);
            }
        }

        public static bool VerifySignature(string hashOfDataToSign, string signature, RSAParameters publicKey)
        {
            return VerifySignature(
                Convert.FromBase64String(hashOfDataToSign),
                Convert.FromBase64String(signature),
                publicKey);
        }

        public static bool VerifySignature(byte[] hashOfDataToSign, byte[] signature, RSAParameters publicKey)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.ImportParameters(publicKey);

                var rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
                rsaDeformatter.SetHashAlgorithm("SHA256");

                return rsaDeformatter.VerifySignature(hashOfDataToSign, signature);
            }
        }
    }

    public class FunCertificate
    {
        public FunCertificate(RSAParameters publicKey, RSAParameters privateKey)
        {
            PublicKey = publicKey;
            PrivateKey = privateKey;
        }

        public RSAParameters PublicKey { get; }
        public RSAParameters PrivateKey { get; }
    }
}
