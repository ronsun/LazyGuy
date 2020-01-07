using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace LazyGuy.Extensions.Tests
{
    [TestFixture()]
    public class SymmetricAlgorithmExtensionsTests
    {
        // NOTE: I'm not sure is it a good idea to encrypt and decrypt back to verify it works correctly or not.

        #region With string input

        [Test()]
        [TestCaseSource(nameof(TestCase_EncryptAndDecryptCorrectly))]
        public void Test_InputString_EncryptAndDecryptCorrectly(SymmetricAlgorithm stubEncryptor, SymmetricAlgorithm stubDecryptor)
        {
            // arrange
            string stubPlaintext = "a";

            // act
            string actualCiphertext = stubEncryptor.Encrypt(stubPlaintext);
            string actualPlaintext = stubDecryptor.Decrypt(actualCiphertext);

            // assert
            actualPlaintext.Should().Be(stubPlaintext);
        }

        [Test()]
        [TestCaseSource(nameof(TestCase_EncryptAndDecryptCorrectly))]
        public void Test_InputStringWithEncoding_EncryptAndDecryptCorrectly(SymmetricAlgorithm stubEncryptor, SymmetricAlgorithm stubDecryptor)
        {
            // arrange
            string stubPlaintext = "a";
            Encoding stubEncoding = Encoding.ASCII;

            // act
            string actualCiphertext = stubEncryptor.Encrypt(stubPlaintext, stubEncoding);
            string actualPlaintext = stubDecryptor.Decrypt(actualCiphertext, stubEncoding);

            // assert
            actualPlaintext.Should().Be(stubPlaintext);
        }

        #endregion

        #region With bytes input


        #endregion

        private static List<object[]> TestCase_EncryptAndDecryptCorrectly()
        {
            return new List<object[]>()
            {
                new object[]
                {
                    new AesCryptoServiceProvider()
                    {
                        Mode = CipherMode.ECB,
                        Key = GenerateKey(128),
                    },
                    new AesCryptoServiceProvider()
                    {
                        Mode = CipherMode.ECB,
                        Key = GenerateKey(128),
                    }
                },

                new object[]
                {
                    new DESCryptoServiceProvider()
                    {
                        Mode = CipherMode.ECB,
                        Key = GenerateKey(64)
                    },
                    new DESCryptoServiceProvider()
                    {
                        Mode = CipherMode.ECB,
                        Key = GenerateKey(64)
                    }
                },
                new object []
                {
                    new RC2CryptoServiceProvider()
                    {
                        Mode = CipherMode.ECB,
                        Key = GenerateKey(64)
                    },
                    new RC2CryptoServiceProvider()
                    {
                        Mode = CipherMode.ECB,
                        Key = GenerateKey(64)
                    }
                },
                new object []
                {
                    new RijndaelManaged()
                    {
                        Mode = CipherMode.ECB,
                        Key = GenerateKey(128),
                    },
                    new RijndaelManaged()
                    {
                        Mode = CipherMode.ECB,
                        Key = GenerateKey(128),
                    }
                },
                new object []
                {
                    new TripleDESCryptoServiceProvider()
                    {
                        Mode = CipherMode.ECB,
                        Key = GenerateKey(192)
                    },
                    new TripleDESCryptoServiceProvider()
                    {
                        Mode = CipherMode.ECB,
                        Key = GenerateKey(192)
                    }
                },
                new object []
                {
                    new TripleDESCng()
                    {
                        Mode = CipherMode.ECB,
                        Key = GenerateKey(192)
                    },
                    new TripleDESCng()
                    {
                        Mode = CipherMode.ECB,
                        Key = GenerateKey(192)
                    }
                },
            };
        }

        private static byte[] GenerateKey(int bits)
        {
            int bytes = bits / 8;
            var key = new byte[bytes];
            new Random(0).NextBytes(key);
            return key;
        }
    }
}