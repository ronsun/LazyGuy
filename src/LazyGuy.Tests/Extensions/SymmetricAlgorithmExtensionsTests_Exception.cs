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
        public void EncryptTest_InputEmptyString_ReturnArgumentNullException()
        {
            // arrange
            string stubPlaintext = null;
            var target = new AesCryptoServiceProvider();

            //act
            Action act = () => { target.Encrypt(stubPlaintext); };

            //assert
            act.Should().Throw<ArgumentNullException>().WithMessage(MessageTemplates.ArgumentNull);
        }

        [Test()]
        public void EncryptTest_InputEmptyString_ReturnArgumentOutOfRangeException()
        {
            // arrange
            string stubPlaintext = string.Empty;
            var target = new AesCryptoServiceProvider();

            //act
            Action act = () => { target.Encrypt(stubPlaintext); };

            //assert
            act.Should().Throw<ArgumentOutOfRangeException>().WithMessage(MessageTemplates.ArgumentEmpty);
        }

        [Test()]
        public void EncryptTest_InputNullSymmetricAlgorithmWithString_ReturnArgumentNullException()
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
        public void EncryptTest_InputNullBytes_ReturnArgumentNullException()
        {
            // arrange
            byte[] stubPlaintextBytes = null;
            var target = new AesCryptoServiceProvider();

            //act
            Action act = () => { target.Encrypt(stubPlaintextBytes); };

            //assert
            act.Should().Throw<ArgumentNullException>().WithMessage(MessageTemplates.ArgumentNull);
        }

        [Test()]
        public void EncryptTest_InputEmptyBytes_ReturnArgumentOutOfRangeException()
        {
            // arrange
            byte[] stubPlaintextBytes = new byte[0];
            var target = new AesCryptoServiceProvider();

            //act
            Action act = () => { target.Encrypt(stubPlaintextBytes); };

            //assert
            act.Should().Throw<ArgumentOutOfRangeException>().WithMessage(MessageTemplates.ArgumentEmpty);
        }

        [Test()]
        public void EncryptTest_InputNullSymmetricAlgorithmWithBytes_ReturnArgumentNullException()
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
        public void DecryptTest_InputNullString_ReturnArgumentNullException()
        {
            // arrange
            string stubCiphertext = null;
            var target = new AesCryptoServiceProvider();

            //act
            Action act = () => { target.Decrypt(stubCiphertext); };

            //assert
            act.Should().Throw<ArgumentNullException>().WithMessage(MessageTemplates.ArgumentNull);
        }

        [Test()]
        public void DecryptTest_InputEmptyString_ReturnArgumentOutOfRangeException()
        {
            // arrange
            string stubCiphertext = string.Empty;
            var target = new AesCryptoServiceProvider();

            //act
            Action act = () => { target.Decrypt(stubCiphertext); };

            //assert
            act.Should().Throw<ArgumentOutOfRangeException>().WithMessage(MessageTemplates.ArgumentEmpty);
        }

        [Test()]
        public void DecryptTest_InputNullSymmetricAlgorithmWithString_ReturnArgumentNullException()
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