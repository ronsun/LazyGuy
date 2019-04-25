using System;
using System.Security.Cryptography;
using FluentAssertions;
using LazyGuy.Tests.Constants;
using NUnit.Framework;

namespace LazyGuy.Extensions.Tests
{
    [TestFixture()]
    public class HashAlgorithmExtensionsTests_Exception
    {
        #region ComputeHashToString with string input

        [Test()]
        [TestCase(null)]
        [TestCase("")]
        public void ComputeHashToStringTest_InputNullOrEmptyString_ThrowArgumentOutOfRangeExceptionWithMessage(string stubPlaintext)
        {
            // arrange
            var target = new MD5CryptoServiceProvider();

            //act
            Action act = () => { target.ComputeHashToString(stubPlaintext); };

            //assert
            act.Should().Throw<ArgumentOutOfRangeException>().WithMessage(MessageTemplates.ArgumentNullOrEmpty);
        }

        [Test()]
        public void ComputeHashToStringTest_InputNullHashAlgorithmWithString_ThrowArgumentNullExceptionWithMessage()
        {
            // arrange
            string stubPlaintext = "a";
            HashAlgorithm target = null;

            //act
            Action act = () => { target.ComputeHashToString(stubPlaintext); };

            //assert
            act.Should().Throw<ArgumentNullException>().WithMessage(MessageTemplates.ArgumentNull);
        }

        #endregion

        #region ComputeHashToString with bytes input

        [Test()]
        [TestCase(null)]
        [TestCase(new byte[0])]
        public void ComputeHashToStringTest_InputNullOrEmptyBytes_ThrowArgumentOutOfRangeExceptionWithMessage(byte[] stubPlaintextBytes)
        {
            // arrange
            var target = new MD5CryptoServiceProvider();

            //act
            Action act = () => { target.ComputeHashToString(stubPlaintextBytes); };

            //assert
            act.Should().Throw<ArgumentOutOfRangeException>().WithMessage(MessageTemplates.ArgumentNullOrEmpty);
        }

        [Test()]
        public void ComputeHashToStringTest_InputNullHashAlgorithmWithBytes_ThrowArgumentNullExceptionWithMessage()
        {
            // arrange
            byte[] stubPlaintextBytes = new byte[] { 0x31 };
            HashAlgorithm target = null;

            //act
            Action act = () => { target.ComputeHashToString(stubPlaintextBytes); };

            //assert
            act.Should().Throw<ArgumentNullException>().WithMessage(MessageTemplates.ArgumentNull);
        }
        #endregion
    }
}