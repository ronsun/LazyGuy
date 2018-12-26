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
        public void EncryptTest_InputEmptyString_ReturnArgumentOutOfRangeException(string mockedPlaintext)
        {
            // arrange
            var target = new AesCryptoServiceProvider();

            //act
            Action act = () => { target.Encrypt(mockedPlaintext); };

            //assert
            act.Should().Throw<ArgumentOutOfRangeException>().WithMessage(FakeMessageTemplates.ArgumentEmpty);
        }

        [Test()]
        public void EncryptTest_InputNullSymmetricAlgorithmWithString_ReturnArgumentNullException()
        {
            // arrange
            string mockedPlaintext = "a";
            SymmetricAlgorithm target = null;

            //act
            Action act = () => { target.Encrypt(mockedPlaintext); };

            //assert
            act.Should().Throw<ArgumentNullException>().WithMessage(FakeMessageTemplates.ArgumentNull);
        }

        #endregion

        #region Encrypt with bytes input

        [TestCase(null)]
        [TestCase(new byte[0])]
        public void EncryptTest_InputNullBytes_ReturnArgumentOutOfRangeException(byte[] mockedPlaintextBytes)
        {
            // arrange
            var target = new AesCryptoServiceProvider();

            //act
            Action act = () => { target.Encrypt(mockedPlaintextBytes); };

            //assert
            act.Should().Throw<ArgumentOutOfRangeException>().WithMessage(FakeMessageTemplates.ArgumentEmpty);
        }


        [Test()]
        public void ComputeHashToStringTest_InputNullSymmetricAlgorithmWithBytes_ReturnArgumentNullException()
        {
            // arrange
            byte[] mockedPlaintextBytes = new byte[] { 0x31 };
            SymmetricAlgorithm target = null;

            //act
            Action act = () => { target.Encrypt(mockedPlaintextBytes); };

            //assert
            act.Should().Throw<ArgumentNullException>().WithMessage(FakeMessageTemplates.ArgumentNull);
        }

        #endregion
    }
}