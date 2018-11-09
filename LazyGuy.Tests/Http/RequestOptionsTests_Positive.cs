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
        private RequestOptions target;

        [Test()]
        public void CreateTest_DefaultOptions_MatchExpected()
        {
            //arrange
            Encoding expectedEncoding = Encoding.UTF8;
            int expectedTimeout = 100000;
            int expectedReadWriteTimeout = 300000;
            string expectedContentType = "application/x-www-form-urlencoded";

            //act
            target = new RequestOptions();

            //assert
            target.Encoding.Should().Be(expectedEncoding);
            target.Timeout.Should().Be(expectedTimeout);
            target.ReadWriteTimeout.Should().Be(expectedReadWriteTimeout);
            target.ContentType.Should().Be(expectedContentType);
            target.Proxy.Should().BeNull();
        }

        [Test()]
        public void SetDefaultProxyTest_CorrectValueInTarget()
        {
            //arrange
            IWebProxy expected = WebRequest.GetSystemWebProxy();
            expected.Credentials = CredentialCache.DefaultCredentials;
            target = new RequestOptions();

            //act
            target.SetDefaultProxy();

            //assert
            target.Proxy.Should().BeEquivalentTo(expected);
        }

        [Test()]
        public void SetProxyTest_CorrectValue()
        {
            //arrange
            string stubAdress = "127.0.0.1";
            string stubUserName = "Ron";
            string stubPassword = "P@ssword";

            var expected = new WebProxy(stubAdress);
            var expectedCredentials = new NetworkCredential(stubUserName, stubPassword);
            expected.Credentials = expectedCredentials;

            target = new RequestOptions();

            //act
            target.SetProxy(stubAdress, stubUserName, stubPassword);
            WebProxy actual = target.Proxy as WebProxy;
            Uri actualAddress = actual.Address;
            NetworkCredential actualCredential = actual.Credentials as NetworkCredential;

            //assert
            actualAddress.Should().BeEquivalentTo(expected.Address);
            actualCredential.Should().BeEquivalentTo(expectedCredentials);
        }
    }
}
