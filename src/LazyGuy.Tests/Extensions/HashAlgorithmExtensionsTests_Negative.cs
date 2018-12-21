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
            string mockedPlaintext = null;
            var target = new MD5CryptoServiceProvider();

            //act
            Action act = () => { target.ComputeHashToString(mockedPlaintext); };

            //assert
            act.Should().Throw<ArgumentNullException>().WithMessage(FakeMessageTemplates.ArgumentNull);
        }

        #endregion

        #region ComputeHashToString with byte array input

        [Test()]
        public void ComputeHashToStringTest_InputNullBytes_ReturnArgumentNullException()
        {
            // arrange
            byte[] mockedPlaintextBytes = null;
            var target = new MD5CryptoServiceProvider();

            //act
            Action act = () => { target.ComputeHashToString(mockedPlaintextBytes); };

            //assert
            act.Should().Throw<ArgumentNullException>().WithMessage(FakeMessageTemplates.ArgumentNull);
        }

        #endregion
    }
}