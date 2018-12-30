using System;
using System.Security.Cryptography;
using FluentAssertions;
using LazyGuy.Tests.Constants;
using NUnit.Framework;

namespace LazyGuy.Extensions.Tests
{
    [TestFixture()]
    public class SymmetricAlgorithmExtensionsTests_Negative
    {
        #region Encrypt with string input

        [TestCase(null)]
        [TestCase("")]
        public void EncryptTest_InputEmptyString_ReturnArgumentOutOfRangeException(string stubPlaintext)
        {
            // arrange
            var target = new AesCryptoServiceProvider();

            //act
            Action act = () => { target.Encrypt(stubPlaintext); };

            //assert
            act.Should().Throw<ArgumentOutOfRangeException>().WithMessage(FakeMessageTemplates.ArgumentEmpty);
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
            act.Should().Throw<ArgumentNullException>().WithMessage(FakeMessageTemplates.ArgumentNull);
        }

        #endregion

        #region Encrypt with bytes input

        [TestCase(null)]
        [TestCase(new byte[0])]
        public void EncryptTest_InputEmptyBytes_ReturnArgumentOutOfRangeException(byte[] stubPlaintextBytes)
        {
            // arrange
            var target = new AesCryptoServiceProvider();

            //act
            Action act = () => { target.Encrypt(stubPlaintextBytes); };

            //assert
            act.Should().Throw<ArgumentOutOfRangeException>().WithMessage(FakeMessageTemplates.ArgumentEmpty);
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
            act.Should().Throw<ArgumentNullException>().WithMessage(FakeMessageTemplates.ArgumentNull);
        }

        #endregion

        #region Decrypt

        [TestCase(null)]
        [TestCase("")]
        public void DecryptTest_InputEmptyString_ReturnArgumentOutOfRangeException(string stubCiphertext)
        {
            // arrange
            var target = new AesCryptoServiceProvider();

            //act
            Action act = () => { target.Decrypt(stubCiphertext); };

            //assert
            act.Should().Throw<ArgumentOutOfRangeException>().WithMessage(FakeMessageTemplates.ArgumentEmpty);
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
            act.Should().Throw<ArgumentNullException>().WithMessage(FakeMessageTemplates.ArgumentNull);
        }

        #endregion
    }
}