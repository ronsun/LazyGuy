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
        /// <param name="plaintext">Plaintext, usd UTF-8 as default encoding </param>
        /// <returns>Hashed string.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="hash"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="plaintext"/> is null or empty string.
        /// </exception>
        public static string ComputeHashToString(this HashAlgorithm hash, string plaintext)
        {
            Argument.NotNull(hash, nameof(hash));
            Argument.NotNullOrEmpty(plaintext, nameof(plaintext));

            return ComputeHashToString(hash, Encoding.UTF8.GetBytes(plaintext));
        }

        /// <summary>
        /// CompteHash and convert result to string without charactor '-'.
        /// </summary>
        /// <param name="hash">The instance of HashAlgorithm. </param>
        /// <param name="plaintextBytes">Plaintext in byte array. </param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="hash"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="plaintextBytes"/> is null or empty string.
        /// </exception>
        public static string ComputeHashToString(this HashAlgorithm hash, byte[] plaintextBytes)
        {
            Argument.NotNull(hash, nameof(hash));
            Argument.NotNullOrEmpty(plaintextBytes, nameof(plaintextBytes));

            byte[] ciphertextBytes = hash.ComputeHash(plaintextBytes);

            return BitConverter.ToString(ciphertextBytes).Replace("-", string.Empty);
        }
    }
}
