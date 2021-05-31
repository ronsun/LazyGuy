using System;
using System.Security.Cryptography;
using System.Text;

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
        /// <param name="plaintext">Plaintext, usd UTF-8 as default encoding. </param>
        /// <returns>Hashed string.</returns>
        public static string ComputeHashToString(this HashAlgorithm hash, string plaintext)
        {
            return ComputeHashToString(hash, Encoding.UTF8.GetBytes(plaintext));
        }

        /// <summary>
        /// CompteHash and convert result to string without charactor '-'.
        /// </summary>
        /// <param name="hash">The instance of HashAlgorithm. </param>
        /// <param name="plaintextBytes">Plaintext in byte array. </param>
        /// <returns>Hashed string.</returns>
        public static string ComputeHashToString(this HashAlgorithm hash, byte[] plaintextBytes)
        {
            byte[] ciphertextBytes = hash.ComputeHash(plaintextBytes);

            return BitConverter.ToString(ciphertextBytes).Replace("-", string.Empty);
        }
    }
}
