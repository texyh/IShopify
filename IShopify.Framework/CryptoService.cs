using IShopify.Core.Framework;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace IShopify.Framework
{
    public class CryptoService : ICryptoService
    {

        public string CreateUniqueKey(int length = 32)
        {
            var bytes = new byte[length];
            new RNGCryptoServiceProvider().GetBytes(bytes);

            return ToHexString(bytes);
        }
        
        public string GenerateSalt(int maxLenght)
        {
            var salt = new byte[maxLenght];

            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }

        public string Hash(string text, string salt = null, int iterations = 1)
        {
            if (salt != null)
            {
                text += salt;
            }

            using (var sha = SHA256Managed.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(text);
                var hash = sha.ComputeHash(bytes);

                for (int i = 1; i < iterations; i++)
                {
                    hash = sha.ComputeHash(hash);
                }

                return ToHexString(hash);
            }
        }

        private static string ToHexString(byte[] byteArray)
        {
            var sb = new StringBuilder();

            foreach (var value in byteArray)
            {
                sb.Append(value.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
