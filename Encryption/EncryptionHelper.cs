using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Encryption
{
    public static class EncryptionHelper
    {
        private static readonly byte[] Key = Encoding.UTF8.GetBytes(
            Environment.GetEnvironmentVariable("ENCRYPTION_KEY") ?? throw new InvalidOperationException("ENCRYPTION_KEY environment variable not set")
        );

        private static readonly byte[] Iv = Encoding.UTF8.GetBytes(
            Environment.GetEnvironmentVariable("ENCRYPTION_IV") ?? throw new InvalidOperationException("ENCRYPTION_IV environment variable not set")
        );

        public static void EncryptFile(string inputFilePath, string outputFilePath)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = Iv;
                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var inputFileStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read))
                using (var outputFileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                using (var cryptoStream = new CryptoStream(outputFileStream, encryptor, CryptoStreamMode.Write))
                {
                    inputFileStream.CopyTo(cryptoStream);
                }
            }
        }

        public static void DecryptFile(string inputFilePath, string outputFilePath)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = Iv;
                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                using (var inputFileStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read))
                using (var outputFileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                using (var cryptoStream = new CryptoStream(inputFileStream, decryptor, CryptoStreamMode.Read))
                {
                    cryptoStream.CopyTo(outputFileStream);
                }
            }
        }
    }
}
