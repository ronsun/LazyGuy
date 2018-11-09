using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace LazyGuy.Http.Tests
{
    [TestFixture()]
    public class HttpHelperTests
    {
        [Test()]
        public void GetTest_DefaultOptions_ParametersOfRequestMatchExpected()
        {
            // arrange
            var stubResponseString = "response";
            var stubUrl = $"http://{nameof(GetTest_DefaultOptions_ParametersOfRequestMatchExpected)}";
            var mockRequest = SetupFakeRequest(stubResponseString, stubUrl);

            var target = new HttpHelper();

            string expectedMethod = "GET";
            int expectedTimeout = 100000;
            int expectedReadWriteTimeout = 300000;
            string expectedContentType = "application/x-www-form-urlencoded";
            IWebProxy expectedProxy = mockRequest.Proxy;

            // act
            target.Get(stubUrl);

            // assert
            mockRequest.Timeout.Should().Be(expectedTimeout);
            mockRequest.ReadWriteTimeout.Should().Be(expectedReadWriteTimeout);
            mockRequest.ContentType.Should().Be(expectedContentType);
            mockRequest.Method.Should().Be(expectedMethod);
            mockRequest.Proxy.Should().BeEquivalentTo(expectedProxy);
        }

        //[Test()]
        //public void GetTest_SetupOptions_ParametersOfRequestMatchExpected()
        //{
        //    // arrange
        //    var stubResponseString = "response";
        //    var stubUrl = "http://url";
        //    var stubOptions = Substitute.For<RequestOptions>();
        //    stubOption
        //    var mockRequest = SetupFakeRequest(stubResponseString, stubUrl);

        //    var target = new HttpHelper();

        //    string expectedMethod = "GET";
        //    int expectedTimeout = 100000;
        //    int expectedReadWriteTimeout = 300000;
        //    string expectedContentType = "application/x-www-form-urlencoded";
        //    IWebProxy expectedProxy = mockRequest.Proxy;

        //    // act
        //    target.Get(stubUrl);

        //    // assert
        //    mockRequest.Timeout.Should().Be(expectedTimeout);
        //    mockRequest.ReadWriteTimeout.Should().Be(expectedReadWriteTimeout);
        //    mockRequest.ContentType.Should().Be(expectedContentType);
        //    mockRequest.Method.Should().Be(expectedMethod);
        //    mockRequest.Proxy.Should().BeEquivalentTo(expectedProxy);
        //}

        [Test()]
        public void GetTest_DefaultOptions_ResponseMatchExpected()
        {
            // arrange
            var stubUrl = $"http://{nameof(GetTest_DefaultOptions_ResponseMatchExpected)}";
            var stubResponseString = "response";

            SetupFakeRequest(stubResponseString, stubUrl);

            var target = new HttpHelper();

            string expected = stubResponseString;
            // act
            var actual = target.Get(stubUrl);

            // assert
            actual.Should().Be(expected);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stubResponseString"></param>
        /// <param name="stubUrl">Must be defference everytime.</param>
        /// <returns></returns>
        private HttpWebRequest SetupFakeRequest(string stubResponseString, string stubUrl)
        {
            var stubResponseStream = new MemoryStream(Encoding.UTF8.GetBytes(stubResponseString));
            var stubResponse = Substitute.For<HttpWebResponse>();
            stubResponse.GetResponseStream().Returns(stubResponseStream);

            var mockRequest = Substitute.ForPartsOf<HttpWebRequest>();
            mockRequest.When(r => r.GetResponse()).DoNotCallBase();
            mockRequest.GetResponse().Returns(stubResponse);
            mockRequest.Headers = new WebHeaderCollection();

            var stubRequestCreater = Substitute.For<IWebRequestCreate>();
            stubRequestCreater.Create(Arg.Any<Uri>()).Returns(mockRequest);
            WebRequest.RegisterPrefix(stubUrl, stubRequestCreater);

            return mockRequest;
        }
    }
}