using FluentAssertions;
using LazyGuy.Http;
using NUnit.Framework;
using System;
using System.Net;
using System.Text;

namespace LazyGuy.Tests.Http
{
    [TestFixture()]
    public class RequestOptionsTests
    {
        private RequestOptions _target;

        [SetUp]
        public void InitRequestOptions()
        {
            _target = RequestOptions.Create();
        }

        [Test()]
        public void CreateTest_DefaultOptions_MatchExpected()
        {
            //arrange
            Encoding expectedEncoding = Encoding.UTF8;
            int expectedTimeout = 100000;
            int expectedReadWriteTimeout = 300000;
            string expectedContentType = "application/x-www-form-urlencoded";

            //act

            //assert
            _target.Encoding.Should().Be(expectedEncoding);
            _target.Timeout.Should().Be(expectedTimeout);
            _target.ReadWriteTimeout.Should().Be(expectedReadWriteTimeout);
            _target.ContentType.Should().Be(expectedContentType);
            _target.Proxy.Should().BeNull();
        }

        [Test()]
        public void WithTimeoutTest_SetValue_CorrectValueInTarget()
        {
            //arrange
            int stubTimeout = 20000;
            int expected = stubTimeout;

            //act
            var actual = _target.WithTimeout(stubTimeout).Timeout;

            //assert
            actual.Should().Be(expected);
        }

        [Test()]
        public void WithReadWriteTimeoutTest_SetValue_CorrectValueInTarget()
        {
            //arrange
            int stubReadWriteTimeout = 99000;
            int expected = stubReadWriteTimeout;

            //act
            var actual = _target.WithReadWriteTimeout(stubReadWriteTimeout).ReadWriteTimeout;

            //assert
            actual.Should().Be(expected);
        }

        [Test()]
        public void WithContentTypeTest_SetValue_CorrectValueInTarget()
        {
            //arrange
            string stubContentType = "my-content-type";
            string expected = stubContentType;

            //act
            var actual = _target.WithContentType(stubContentType).ContentType;

            //assert
            actual.Should().Be(expected);
        }

        [Test()]
        public void WithEncodingTest_SetValue_CorrectValueInTarget()
        {
            //arrange
            Encoding stubEncoding = Encoding.Unicode;
            Encoding expected = stubEncoding;

            //act
            var actual = _target.WithEncoding(stubEncoding).Encoding;

            //assert
            actual.Should().Be(expected);
        }

        [Test()]
        public void WithProxyTest_SetValueWithoutArgs_CorrectValueInTarget()
        {
            //arrange
            IWebProxy expected = WebRequest.GetSystemWebProxy();
            expected.Credentials = CredentialCache.DefaultCredentials;

            //act
            var actual = _target.WithProxy().Proxy;

            //assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test()]
        public void WithProxyTest_WithArgs_CorrectValue()
        {
            //arrange
            string stubAdress = "127.0.0.1";
            string stubUserName = "Ron";
            string stubPassword = "P@ssword";

            var expected = new WebProxy(stubAdress);
            var expectedCredentials = new NetworkCredential(stubUserName, stubPassword);
            expected.Credentials = expectedCredentials;

            //act
            RequestOptions requestOptions = _target.WithProxy(stubAdress, stubUserName, stubPassword);
            WebProxy actual = requestOptions.Proxy as WebProxy;
            Uri actualAddress = actual.Address;
            NetworkCredential actualCredential = actual.Credentials as NetworkCredential;

            //assert
            actualAddress.Should().BeEquivalentTo(expected.Address);
            actualCredential.Should().BeEquivalentTo(expectedCredentials);
        }
    }
}
