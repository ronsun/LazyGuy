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
        #region General

        [Test()]
        [TestCase(WebRequestMethods.Http.Get)]
        [TestCase(WebRequestMethods.Http.Post)]
        public void GeneralTest_DefaultOptions_RequestOptionsIsDefault(string httpVerb)
        {
            // arrange
            var stubPostConent = string.Empty;
            var stubUrl = $"http://{nameof(GeneralTest_DefaultOptions_RequestOptionsIsDefault)}/{httpVerb}";
            var mockRequest = InitFakeRequest(stubUrl);

            var target = new HttpHelper();

            var expectedOptions = new RequestOptions();
            string expectedMethod = httpVerb;
            IWebProxy expectedProxy = mockRequest.Proxy;

            // act
            if (httpVerb == WebRequestMethods.Http.Get)
            {
                target.Get(stubUrl);
            }
            else if (httpVerb == WebRequestMethods.Http.Post)
            {
                target.Post(stubUrl, stubPostConent);
            }
            var actual = target.CurrentRequest;

            // assert
            actual.Timeout.Should().Be(expectedOptions.Timeout);
            actual.ReadWriteTimeout.Should().Be(expectedOptions.ReadWriteTimeout);
            actual.ContentType.Should().Be(expectedOptions.ContentType);
            actual.Method.Should().Be(expectedMethod);
            actual.Proxy.Should().BeEquivalentTo(expectedProxy);
        }

        [Test()]
        [TestCase(WebRequestMethods.Http.Get)]
        [TestCase(WebRequestMethods.Http.Post)]
        public void GeneralTest_DefaultOptions_ResponseMatchExpected(string httpVerb)
        {
            // arrange
            var stubPostConent = string.Empty;
            var stubUrl = $"http://{nameof(GeneralTest_DefaultOptions_ResponseMatchExpected)}/{httpVerb}";
            var stubResponseString = "response";

            InitFakeRequest(stubUrl, stubResponseString);

            var target = new HttpHelper();

            string expected = stubResponseString;

            // act
            string actual = null;
            if (httpVerb == WebRequestMethods.Http.Get)
            {
                actual = target.Get(stubUrl);
            }
            else if (httpVerb == WebRequestMethods.Http.Post)
            {
                actual = target.Post(stubUrl, stubPostConent);
            }

            // assert
            actual.Should().NotBeNull();
            actual.Should().Be(expected);
        }

        [Test()]
        [TestCase(WebRequestMethods.Http.Get)]
        [TestCase(WebRequestMethods.Http.Post)]
        public void GeneralTest_SetupOptions_ParametersOfRequestMatchExpected(string httpVerb)
        {
            // arrange
            var stubPostConent = string.Empty;
            var stubUrl = $"http://{nameof(GeneralTest_SetupOptions_ParametersOfRequestMatchExpected)}/{httpVerb}";
            var stubOptions = new RequestOptions()
            {
                Encoding = Encoding.Default,
                Timeout = 1,
                ReadWriteTimeout = 2,
                ContentType = "contentType"
            };
            stubOptions.SetDefaultProxy();

            var mockRequest = InitFakeRequest(stubUrl);
            IWebProxy argumentProxy = null;
            mockRequest.When(r => r.Proxy = stubOptions.Proxy)
                       .Do(calledMethod => argumentProxy = calledMethod.ArgAt<IWebProxy>(0));

            var target = new HttpHelper();

            string expectedMethod = httpVerb;
            int expectedTimeout = stubOptions.Timeout;
            int expectedReadWriteTimeout = stubOptions.ReadWriteTimeout;
            string expectedContentType = stubOptions.ContentType;
            var expectedProxy = stubOptions.Proxy;

            // act
            if (httpVerb == WebRequestMethods.Http.Get)
            {
                target.Get(stubUrl, stubOptions);
            }
            else if (httpVerb == WebRequestMethods.Http.Post)
            {
                target.Post(stubUrl, stubPostConent, stubOptions);
            }
            var actual = target.CurrentRequest;
            var actualProxy = argumentProxy;

            // assert
            actual.Timeout.Should().Be(expectedTimeout);
            actual.ReadWriteTimeout.Should().Be(expectedReadWriteTimeout);
            actual.ContentType.Should().Be(expectedContentType);
            actual.Method.Should().Be(expectedMethod);
            actualProxy.Should().Be(expectedProxy);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stubResponseString"></param>
        /// <param name="stubUrl">Must be defference everytime.</param>
        /// <returns></returns>
        private HttpWebRequest InitFakeRequest(string stubUrl, string stubResponseString = "response")
        {
            var stubResponseStream = new MemoryStream(Encoding.UTF8.GetBytes(stubResponseString));
            var stubResponse = Substitute.For<HttpWebResponse>();
            stubResponse.GetResponseStream().Returns(stubResponseStream);

            var mockRequest = Substitute.ForPartsOf<HttpWebRequest>();
            mockRequest.When(r => r.GetResponse()).DoNotCallBase();
            mockRequest.GetResponse().Returns(stubResponse);
            mockRequest.When(r => r.GetRequestStream()).DoNotCallBase();
            mockRequest.GetRequestStream().Returns(new MemoryStream());
            mockRequest.When(r => r.Proxy = Arg.Any<IWebProxy>()).DoNotCallBase();
            mockRequest.Headers = new WebHeaderCollection();

            var stubRequestCreater = Substitute.For<IWebRequestCreate>();
            stubRequestCreater.Create(Arg.Any<Uri>()).Returns(mockRequest);
            WebRequest.RegisterPrefix(stubUrl, stubRequestCreater);

            return mockRequest;
        }
    }
}