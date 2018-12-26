using System;
using System.Security.Cryptography;
using System.Text;
using LazyGuy.Constants;

namespace LazyGuy.Extensions
{
    /// <summary>
    /// Extension methods for HashAlgorithm.
    /// </summary>
    public static class HashAlgorithmExtensions
    {
        /// <summary>
        /// CompteHash and convert result to string without charactor '-'.
        /// </summary>
        /// <param name="hash">The instance of HashAlgorithm. </param>
        /// <param name="plaintext">Plaintext. </param>
        /// <returns></returns>
        public static string ComputeHashToString(this HashAlgorithm hash, string plaintext)
        {
            if (hash == null)
            {
                string msg = string.Format(MessageTemplates.ArgumentNull, nameof(hash));
                throw new ArgumentNullException(msg);
            }

            if (string.IsNullOrEmpty(plaintext))
            {
                string msg = string.Format(MessageTemplates.ArgumentEmpty, nameof(plaintext));
                throw new ArgumentOutOfRangeException(msg);
            }

            return ComputeHashToString(hash, Encoding.UTF8.GetBytes(plaintext));
        }

        /// <summary>
        /// CompteHash and convert result to string without charactor '-'.
        /// </summary>
        /// <param name="hash">The instance of HashAlgorithm. </param>
        /// <param name="plaintextBytes">Plaintext in byte array. </param>
        /// <returns></returns>
        public static string ComputeHashToString(this HashAlgorithm hash, byte[] plaintextBytes)
        {
            if (hash == null)
            {
                string msg = string.Format(MessageTemplates.ArgumentNull, nameof(hash));
                throw new ArgumentNullException(msg);
            }

            if (plaintextBytes == null || plaintextBytes.Length == 0)
            {
                string msg = string.Format(MessageTemplates.ArgumentEmpty, nameof(plaintextBytes));
                throw new ArgumentOutOfRangeException(msg);
            }

            byte[] ciphertextBytes = hash.ComputeHash(plaintextBytes);

            return BitConverter.ToString(ciphertextBytes).Replace("-", string.Empty);
        }
    }
}
