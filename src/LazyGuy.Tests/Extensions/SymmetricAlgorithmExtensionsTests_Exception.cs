using System;
using System.Security.Cryptography;
using FluentAssertions;
using LazyGuy.Tests.Constants;
using NUnit.Framework;

namespace LazyGuy.Extensions.Tests
{
    [TestFixture()]
    public class SymmetricAlgorithmExtensionsTests_Exception
    {
        #region Encrypt with string input

        [Test()]
        [TestCase(null)]
        [TestCase("")]
        public void EncryptTest_InputNullOrEmptyString_ThrowArgumentOutOfRangeExceptionWithMessage(string stubPlaintext)
        {
            // arrange
            var target = new AesCryptoServiceProvider();

            //act
            Action act = () => { target.Encrypt(stubPlaintext); };

            //assert
            act.Should().Throw<ArgumentOutOfRangeException>().WithMessage(MessageTemplates.ArgumentNullOrEmpty);
        }

        [Test()]
        public void EncryptTest_InputNullSymmetricAlgorithmWithString_ThrowArgumentNullExceptionWithMessage()
        {
            // arrange
            string stubPlaintext = "a";
            SymmetricAlgorithm target = null;

            //act
            Action act = () => { target.Encrypt(stubPlaintext); };

            //assert
            act.Should().Throw<ArgumentNullException>().WithMessage(MessageTemplates.ArgumentNull);
        }

        #endregion

        #region Encrypt with bytes input

        [Test()]
        [TestCase(null)]
        [TestCase(new byte[0])]
        public void EncryptTest_InputNullOrEmptyBytes_ThrowArgumentOutOfRangeExceptionWithMessage(byte[] stubPlaintextBytes)
        {
            // arrange
            var target = new AesCryptoServiceProvider();

            //act
            Action act = () => { target.Encrypt(stubPlaintextBytes); };

            //assert
            act.Should().Throw<ArgumentOutOfRangeException>().WithMessage(MessageTemplates.ArgumentNullOrEmpty);
        }

        [Test()]
        public void EncryptTest_InputNullSymmetricAlgorithmWithBytes_ThrowArgumentNullExceptionWithMessage()
        {
            // arrange
            byte[] stubPlaintextBytes = new byte[] { 0x31 };
            SymmetricAlgorithm target = null;

            //act
            Action act = () => { target.Encrypt(stubPlaintextBytes); };

            //assert
            act.Should().Throw<ArgumentNullException>().WithMessage(MessageTemplates.ArgumentNull);
        }

        #endregion

        #region Decrypt

        [Test()]
        [TestCase(null)]
        [TestCase("")]
        public void DecryptTest_InputNullOrEmptyString_ThrowArgumentOutOfRangeExceptionWithMessage(string stubCiphertext)
        {
            // arrange
            var target = new AesCryptoServiceProvider();

            //act
            Action act = () => { target.Decrypt(stubCiphertext); };

            //assert
            act.Should().Throw<ArgumentOutOfRangeException>().WithMessage(MessageTemplates.ArgumentNullOrEmpty);
        }

        [Test()]
        public void DecryptTest_InputNullSymmetricAlgorithmWithString_ThrowArgumentNullExceptionWithMessage()
        {
            // arrange
            string stubCiphertext = "a";
            SymmetricAlgorithm target = null;

            //act
            Action act = () => { target.Decrypt(stubCiphertext); };

            //assert
            act.Should().Throw<ArgumentNullException>().WithMessage(MessageTemplates.ArgumentNull);
        }

        #endregion
    }
}