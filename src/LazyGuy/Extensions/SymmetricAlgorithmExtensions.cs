using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using LazyGuy.Constants;

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
        public static string Encrypt(this SymmetricAlgorithm symmetric, string plaintext)
        {
            if (symmetric == null)
            {
                string msg = string.Format(MessageTemplates.ArgumentNull, nameof(symmetric));
                throw new ArgumentNullException(msg);
            }

            if (string.IsNullOrEmpty(plaintext))
            {
                string msg = string.Format(MessageTemplates.ArgumentEmpty, nameof(plaintext));
                throw new ArgumentOutOfRangeException(msg);
            }

            return Encrypt(symmetric, Encoding.UTF8.GetBytes(plaintext));
        }

        /// <summary>
        /// Encrypt.
        /// </summary>
        /// <param name="symmetric">The instance of SymmetricAlgorithm. </param>
        /// <param name="plaintextBytes">The plaintext in byte array. </param>
        /// <returns></returns>
        public static string Encrypt(this SymmetricAlgorithm symmetric, byte[] plaintextBytes)
        {
            if (symmetric == null)
            {
                string msg = string.Format(MessageTemplates.ArgumentNull, nameof(symmetric));
                throw new ArgumentNullException(msg);
            }

            if (plaintextBytes == null || plaintextBytes.Length == 0)
            {
                string msg = string.Format(MessageTemplates.ArgumentEmpty, nameof(plaintextBytes));
                throw new ArgumentOutOfRangeException(msg);
            }

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
            if (symmetric == null)
            {
                string msg = string.Format(MessageTemplates.ArgumentNull, nameof(symmetric));
                throw new ArgumentNullException(msg);
            }

            if (string.IsNullOrEmpty(ciphertext))
            {
                string msg = string.Format(MessageTemplates.ArgumentEmpty, nameof(ciphertext));
                throw new ArgumentOutOfRangeException(msg);
            }

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
