using FluentAssertions;
using NUnit.Framework;
using System.Collections;
using System.Security.Cryptography;

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

        private static IEnumerable TestCase_ComputeHashToStringTest_ReturnWithoutHyphen()
        {
            byte[] hmacKeyBytes = new byte[] { 0x00 };

            // CryptoServiceProvider
            yield return new TestCaseData(new MD5CryptoServiceProvider());
            yield return new TestCaseData(new SHA1CryptoServiceProvider());
            yield return new TestCaseData(new SHA256CryptoServiceProvider());
            yield return new TestCaseData(new SHA384CryptoServiceProvider());
            yield return new TestCaseData(new SHA512CryptoServiceProvider());

            // hmac series
            yield return new TestCaseData(new HMACMD5(hmacKeyBytes));
            yield return new TestCaseData(new HMACSHA1(hmacKeyBytes));
            yield return new TestCaseData(new HMACSHA256(hmacKeyBytes));
            yield return new TestCaseData(new HMACSHA384(hmacKeyBytes));
            yield return new TestCaseData(new HMACSHA512(hmacKeyBytes));
            yield return new TestCaseData(new HMACRIPEMD160(hmacKeyBytes));


            // managed series
            yield return new TestCaseData(new SHA1Managed());
            yield return new TestCaseData(new SHA256Managed());
            yield return new TestCaseData(new SHA384Managed());
            yield return new TestCaseData(new SHA512Managed());
            yield return new TestCaseData(new RIPEMD160Managed());

            // cng series
            yield return new TestCaseData(new MD5Cng());
            yield return new TestCaseData(new SHA1Cng());
            yield return new TestCaseData(new SHA256Cng());
            yield return new TestCaseData(new SHA384Cng());
            yield return new TestCaseData(new SHA512Cng());
        }

        private static IEnumerable TestCase_ComputeHashToStringTest_ReturnWithExpectedLength()
        {
            byte[] hmacKeyBytes = new byte[] { 0x00 };

            // CryptoServiceProvider
            yield return new TestCaseData(new MD5CryptoServiceProvider(), 128 / 4);
            yield return new TestCaseData(new SHA1CryptoServiceProvider(), 160 / 4);
            yield return new TestCaseData(new SHA256CryptoServiceProvider(), 256 / 4);
            yield return new TestCaseData(new SHA384CryptoServiceProvider(), 384 / 4);
            yield return new TestCaseData(new SHA512CryptoServiceProvider(), 512 / 4);

            // hmac series
            yield return new TestCaseData(new HMACMD5(hmacKeyBytes), 128 / 4);
            yield return new TestCaseData(new HMACSHA1(hmacKeyBytes), 160 / 4);
            yield return new TestCaseData(new HMACSHA256(hmacKeyBytes), 256 / 4);
            yield return new TestCaseData(new HMACSHA384(hmacKeyBytes), 384 / 4);
            yield return new TestCaseData(new HMACSHA512(hmacKeyBytes), 512 / 4);
            yield return new TestCaseData(new HMACRIPEMD160(hmacKeyBytes), 160 / 4);

            // managed series
            yield return new TestCaseData(new SHA1Managed(), 160 / 4);
            yield return new TestCaseData(new SHA256Managed(), 256 / 4);
            yield return new TestCaseData(new SHA384Managed(), 384 / 4);
            yield return new TestCaseData(new SHA512Managed(), 512 / 4);
            yield return new TestCaseData(new RIPEMD160Managed(), 160 / 4);

            // cng series
            yield return new TestCaseData(new MD5Cng(), 128 / 4);
            yield return new TestCaseData(new SHA1Cng(), 160 / 4);
            yield return new TestCaseData(new SHA256Cng(), 256 / 4);
            yield return new TestCaseData(new SHA384Cng(), 384 / 4);
            yield return new TestCaseData(new SHA512Cng(), 512 / 4);
        }
    }
}