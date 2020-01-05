using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LazyGuy.Extensions
{
    /// <summary>
    /// Extension methods for SymmetricAlgorithm
    /// </summary>
    public static class SymmetricAlgorithmExtensions
    {
        /// <summary>
        /// Encrypt.
        /// </summary>
        /// <param name="symmetric">The instance of SymmetricAlgorithm. </param>
        /// <param name="plaintext">The plaintext string. </param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="symmetric"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="plaintext"/> is null or empty.
        /// </exception>
        public static string Encrypt(this SymmetricAlgorithm symmetric, string plaintext, Encoding encoding = null)
        {
            Argument.NotNull(symmetric, nameof(symmetric));
            Argument.NotNullOrEmpty(plaintext, nameof(plaintext));

            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            return Encrypt(symmetric, encoding.GetBytes(plaintext));
        }

        /// <summary>
        /// Encrypt.
        /// </summary>
        /// <param name="symmetric">The instance of SymmetricAlgorithm. </param>
        /// <param name="plaintextBytes">The plaintext in byte array. </param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="symmetric"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="plaintextBytes"/> is null or empty.
        /// </exception>
        public static string Encrypt(this SymmetricAlgorithm symmetric, byte[] plaintextBytes)
        {
            Argument.NotNull(symmetric, nameof(symmetric));
            Argument.NotNullOrEmpty(plaintextBytes, nameof(plaintextBytes));

            string ciphertext = string.Empty;

            MemoryStream ms = new MemoryStream();
            using (CryptoStream cs = new CryptoStream(ms, symmetric.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(plaintextBytes, 0, plaintextBytes.Length);
                cs.FlushFinalBlock();
                ciphertext = Convert.ToBase64String(ms.ToArray());
            }
            return ciphertext;
        }

        /// <summary>
        /// Decrypt.
        /// </summary>
        /// <param name="symmetric">The instance of SymmetricAlgorithm. </param>
        /// <param name="ciphertext">The ciphertext string. </param>
        /// <param name="plaintextEncoding">Encoding of plaintext. </param>
        /// <returns></returns>
        public static string Decrypt(this SymmetricAlgorithm symmetric, string ciphertext, Encoding plaintextEncoding = null)
        {
            Argument.NotNull(symmetric, nameof(symmetric));
            Argument.NotNullOrEmpty(ciphertext, nameof(ciphertext));

            if (plaintextEncoding == null)
            {
                plaintextEncoding = Encoding.UTF8;
            }

            string plaintext = string.Empty;

            MemoryStream ms = new MemoryStream();
            using (CryptoStream cs = new CryptoStream(ms, symmetric.CreateDecryptor(), CryptoStreamMode.Write))
            {
                var ciphertextBytes = Convert.FromBase64String(ciphertext);

                cs.Write(ciphertextBytes, 0, ciphertextBytes.Length);
                cs.FlushFinalBlock();
                plaintext = plaintextEncoding.GetString(ms.ToArray());
            }
            return plaintext;
        }
    }
}
