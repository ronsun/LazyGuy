using System;
using System.Security.Cryptography;
using FluentAssertions;
using LazyGuy.Tests.Constants;
using NUnit.Framework;
using NSubstitute;
using LazyGuy.Extensions;
using System.Text;
using System.Collections.Generic;

namespace LazyGuy.Extensions.Tests
{
    [TestFixture()]
    public class HashAlgorithmExtensionsTests_Positive
    {
        #region ComputeHashToString with string input

        [Test()]
        [TestCaseSource(nameof(TestCase_ComputeHashToStringTest_ReturnWithoutHyphen))]
        public void ComputeHashToStringTest_InputString_ReturnWithoutHyphen(HashAlgorithm target)
        {
            // arrange
            string stubPlaintext = "a";
            string stubHyphen = "-";

            // act
            string actual = target.ComputeHashToString(stubPlaintext);

            // assert
            actual.Should().NotContain(stubHyphen);
        }

        #endregion

        #region ComputeHashToString with bytes input

        [Test()]
        [TestCaseSource(nameof(TestCase_ComputeHashToStringTest_ReturnWithoutHyphen))]
        public void ComputeHashToStringTest_InputBytes_ReturnWithoutHyphen(HashAlgorithm target)
        {
            // arrange
            byte[] stubPlaintextBytes = new byte[] { 0x00 };
            string stubHyphen = "-";

            // act
            string actual = target.ComputeHashToString(stubPlaintextBytes);

            // assert
            actual.Should().NotContain(stubHyphen);
        }

        #endregion

        private static List<object[]> TestCase_ComputeHashToStringTest_ReturnWithoutHyphen()
        {
            byte[] hmacKeyBytes = new byte[] { 0x00 };

            return new List<object[]>()
            {
                // CryptoServiceProvider
                new object[] { new MD5CryptoServiceProvider() },
                new object[] { new SHA1CryptoServiceProvider() },
                new object[] { new SHA256CryptoServiceProvider() },
                new object[] { new SHA384CryptoServiceProvider() },
                new object[] { new SHA512CryptoServiceProvider() },
                
                // hmac series
                new object[] { new HMACMD5(hmacKeyBytes) },
                new object[] { new HMACSHA1(hmacKeyBytes) },
                new object[] { new HMACSHA256(hmacKeyBytes) },
                new object[] { new HMACSHA384(hmacKeyBytes) },
                new object[] { new HMACSHA512(hmacKeyBytes) },
                new object[] { new HMACRIPEMD160(hmacKeyBytes) },

                // managed series
                new object[] { new SHA1Managed() },
                new object[] { new SHA256Managed() },
                new object[] { new SHA384Managed() },
                new object[] { new SHA512Managed() },
                new object[] { new RIPEMD160Managed() },

                // cng series
                new object[] { new MD5Cng() },
                new object[] { new SHA1Cng() },
                new object[] { new SHA256Cng() },
                new object[] { new SHA384Cng() },
                new object[] { new SHA512Cng() },
            };
        }
    }
}