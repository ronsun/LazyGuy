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

        [Test()]
        public void ComputeHashToStringTest_InputNullString_ReturnArgumentNullException()
        {
            // arrange
            string stubPlaintext = null;
            var target = new MD5CryptoServiceProvider();

            //act
            Action act = () => { target.ComputeHashToString(stubPlaintext); };

            //assert
            act.Should().Throw<ArgumentNullException>().WithMessage(MessageTemplates.ArgumentNull);
        }

        [Test()]
        public void ComputeHashToStringTest_InputEmptyString_ReturnArgumentOutOfRangeException()
        {
            // arrange
            string stubPlaintext = string.Empty;
            var target = new MD5CryptoServiceProvider();

            //act
            Action act = () => { target.ComputeHashToString(stubPlaintext); };

            //assert
            act.Should().Throw<ArgumentOutOfRangeException>().WithMessage(MessageTemplates.ArgumentEmpty);
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
            act.Should().Throw<ArgumentNullException>().WithMessage(MessageTemplates.ArgumentNull);
        }

        #endregion

        #region ComputeHashToString with bytes input

        [Test()]
        public void ComputeHashToStringTest_InputNullBytes_ReturnArgumentNullException()
        {
            // arrange
            byte[] stubPlaintextBytes = null;
            var target = new MD5CryptoServiceProvider();

            //act
            Action act = () => { target.ComputeHashToString(stubPlaintextBytes); };

            //assert
            act.Should().Throw<ArgumentNullException>().WithMessage(MessageTemplates.ArgumentNull);
        }

        [Test()]
        public void ComputeHashToStringTest_InputEmptyBytes_ReturnArgumentOutOfRangeException()
        {
            // arrange
            byte[] stubPlaintextBytes = new byte[0];
            var target = new MD5CryptoServiceProvider();

            //act
            Action act = () => { target.ComputeHashToString(stubPlaintextBytes); };

            //assert
            act.Should().Throw<ArgumentOutOfRangeException>().WithMessage(MessageTemplates.ArgumentEmpty);
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
            act.Should().Throw<ArgumentNullException>().WithMessage(MessageTemplates.ArgumentNull);
        }
        #endregion
    }
}