using System.Collections.Generic;
using System.Security.Cryptography;
using FluentAssertions;
using NUnit.Framework;

namespace LazyGuy.Extensions.Tests
{
    [TestFixture()]
    public class HashAlgorithmExtensionsTests
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

        [Test()]
        [TestCaseSource(nameof(TestCase_ComputeHashToStringTest_ReturnWithExpectedLength))]
        public void ComputeHashToStringTest_InputString_ReturnWithExpectedLength(HashAlgorithm target, int expectedLength)
        {
            // arrange
            string stubPlaintext = "a";

            // act
            string actual = target.ComputeHashToString(stubPlaintext);

            // assert
            actual.Should().HaveLength(expectedLength);
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

        [Test()]
        [TestCaseSource(nameof(TestCase_ComputeHashToStringTest_ReturnWithExpectedLength))]
        public void ComputeHashToStringTest_InputBytes_ReturnWithExpectedLength(HashAlgorithm target, int expectedLength)
        {
            // arrange
            byte[] stubPlaintextBytes = new byte[] { 0x00 };

            // act
            string actual = target.ComputeHashToString(stubPlaintextBytes);

            // assert
            actual.Should().HaveLength(expectedLength);
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

        private static List<object[]> TestCase_ComputeHashToStringTest_ReturnWithExpectedLength()
        {
            byte[] hmacKeyBytes = new byte[] { 0x00 };

            return new List<object[]>()
            {
                // CryptoServiceProvider
                new object[] { new MD5CryptoServiceProvider(), 128 / 4 },
                new object[] { new SHA1CryptoServiceProvider(), 160 / 4 },
                new object[] { new SHA256CryptoServiceProvider(), 256 / 4 },
                new object[] { new SHA384CryptoServiceProvider(), 384 / 4 },
                new object[] { new SHA512CryptoServiceProvider(), 512 / 4 },
                
                // hmac series
                new object[] { new HMACMD5(hmacKeyBytes), 128 / 4 },
                new object[] { new HMACSHA1(hmacKeyBytes), 160 / 4 },
                new object[] { new HMACSHA256(hmacKeyBytes), 256 / 4  },
                new object[] { new HMACSHA384(hmacKeyBytes), 384 / 4  },
                new object[] { new HMACSHA512(hmacKeyBytes), 512 / 4  },
                new object[] { new HMACRIPEMD160(hmacKeyBytes), 160 / 4 },

                // managed series
                new object[] { new SHA1Managed(), 160 / 4  },
                new object[] { new SHA256Managed(), 256 / 4  },
                new object[] { new SHA384Managed(), 384 / 4  },
                new object[] { new SHA512Managed(), 512 / 4  },
                new object[] { new RIPEMD160Managed(), 160 / 4 },

                // cng series
                new object[] { new MD5Cng(), 128 / 4 },
                new object[] { new SHA1Cng(), 160 / 4 },
                new object[] { new SHA256Cng(), 256 / 4 },
                new object[] { new SHA384Cng(), 384 / 4 },
                new object[] { new SHA512Cng(), 512 / 4 },
            };
        }
    }
}