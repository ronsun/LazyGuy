using System;
using System.Security.Cryptography;
using FluentAssertions;
using LazyGuy.Tests.Constants;
using NUnit.Framework;

namespace LazyGuy.Extensions.Tests
{
    [TestFixture()]
    public class HashAlgorithmExtensionsTests_Negative
    {
        #region ComputeHashToString with string input

        [TestCase(null)]
        [TestCase("")]
        public void ComputeHashToStringTest_InputEmptyString_ReturnArgumentOutOfRangeException(string stubPlaintext)
        {
            // arrange
            var target = new MD5CryptoServiceProvider();

            //act
            Action act = () => { target.ComputeHashToString(stubPlaintext); };

            //assert
            act.Should().Throw<ArgumentOutOfRangeException>().WithMessage(FakeMessageTemplates.ArgumentEmpty);
        }

        [Test()]
        public void ComputeHashToStringTest_InputNullHashAlgorithmWithString_ReturnArgumentNullException()
        {
            // arrange
            string stubPlaintext = "a";
            HashAlgorithm target = null;

            //act
            Action act = () => { target.ComputeHashToString(stubPlaintext); };

            //assert
            act.Should().Throw<ArgumentNullException>().WithMessage(FakeMessageTemplates.ArgumentNull);
        }

        #endregion

        #region ComputeHashToString with bytes input

        [TestCase(null)]
        [TestCase(new byte[0])]
        public void ComputeHashToStringTest_InputEmptyBytes_ArgumentOutOfRangeException(byte[] stubPlaintextBytes)
        {
            // arrange
            var target = new MD5CryptoServiceProvider();

            //act
            Action act = () => { target.ComputeHashToString(stubPlaintextBytes); };

            //assert
            act.Should().Throw<ArgumentOutOfRangeException>().WithMessage(FakeMessageTemplates.ArgumentEmpty);
        }

        [Test()]
        public void ComputeHashToStringTest_InputNullHashAlgorithmWithBytes_ReturnArgumentNullException()
        {
            // arrange
            byte[] stubPlaintextBytes = new byte[] { 0x31 };
            HashAlgorithm target = null;

            //act
            Action act = () => { target.ComputeHashToString(stubPlaintextBytes); };

            //assert
            act.Should().Throw<ArgumentNullException>().WithMessage(FakeMessageTemplates.ArgumentNull);
        }
        #endregion
    }
}