﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LazyGuy.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="SymmetricAlgorithm"/>.
    /// </summary>
    public static class SymmetricAlgorithmExtensions
    {
        /// <summary>
        /// Encrypt.
        /// </summary>
        /// <param name="symmetric">The instance of SymmetricAlgorithm. </param>
        /// <param name="plaintext">The plaintext string. </param>
        /// <returns>Ciphertext. </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="symmetric"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="plaintext"/> is null or empty.
        /// </exception>
        public static string Encrypt(this SymmetricAlgorithm symmetric, string plaintext)
        {
            return Encrypt(symmetric, plaintext, Encoding.UTF8);
        }

        /// <summary>
        /// Encrypt.
        /// </summary>
        /// <param name="symmetric">The instance of SymmetricAlgorithm. </param>
        /// <param name="plaintext">The plaintext string. </param>
        /// <param name="plaintextEncoding">Encoding of plaintext. </param>
        /// <returns>Ciphertext. </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="symmetric"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="plaintext"/> is null or empty.
        /// </exception>
        public static string Encrypt(this SymmetricAlgorithm symmetric, string plaintext, Encoding plaintextEncoding)
        {
            return Encrypt(symmetric, plaintextEncoding.GetBytes(plaintext));
        }

        /// <summary>
        /// Encrypt.
        /// </summary>
        /// <param name="symmetric">The instance of SymmetricAlgorithm. </param>
        /// <param name="plaintextBytes">The plaintext in byte array. </param>
        /// <returns>Ciphertext. </returns>
        public static string Encrypt(this SymmetricAlgorithm symmetric, byte[] plaintextBytes)
        {
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
        /// <returns>Plaintext. </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="symmetric"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="ciphertext"/> is null or empty.
        /// </exception>
        public static string Decrypt(this SymmetricAlgorithm symmetric, string ciphertext)
        {
            return Decrypt(symmetric, ciphertext, Encoding.UTF8);
        }

        /// <summary>
        /// Decrypt.
        /// </summary>
        /// <param name="symmetric">The instance of SymmetricAlgorithm. </param>
        /// <param name="ciphertext">The ciphertext string. </param>
        /// <param name="plaintextEncoding">Encoding of plaintext. </param>
        /// <returns>Plaintext. </returns>
        public static string Decrypt(this SymmetricAlgorithm symmetric, string ciphertext, Encoding plaintextEncoding)
        {
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
