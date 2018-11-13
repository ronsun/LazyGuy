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
            var mockRequest = SetupFakeRequest(stubUrl);

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

            SetupFakeRequest(stubUrl, stubResponseString);

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
        public void GeneralTest_SetupOptionsWithoutProxy_ParametersOfRequestMatchExpected(string httpVerb)
        {
            // arrange
            var stubPostConent = string.Empty;
            var stubUrl = $"http://{nameof(GeneralTest_SetupOptionsWithoutProxy_ParametersOfRequestMatchExpected)}/{httpVerb}";
            var stubOptions = new RequestOptions()
            {
                Encoding = Encoding.Default,
                Timeout = 1,
                ReadWriteTimeout = 2,
                ContentType = "contentType"
            };

            var mockRequest = SetupFakeRequest(stubUrl);

            var target = new HttpHelper();

            string expectedMethod = httpVerb;
            int expectedTimeout = stubOptions.Timeout;
            int expectedReadWriteTimeout = stubOptions.ReadWriteTimeout;
            string expectedContentType = stubOptions.ContentType;

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

            // assert
            actual.Timeout.Should().Be(expectedTimeout);
            actual.ReadWriteTimeout.Should().Be(expectedReadWriteTimeout);
            actual.ContentType.Should().Be(expectedContentType);
            actual.Method.Should().Be(expectedMethod);
        }

        /// <summary>
        /// In one sentence, I expected system throw an excpetion in this cas.
        /// 
        /// More detail: 
        /// I mock HttpWebRequest by mock framework instead of static method "Create()" 
        /// provided by WebRequest, so the value of some properties inside the mocked 
        /// HttpWebRequest instance are missing (in this case, missing "Address").
        /// 
        /// Therefore, system throw an exception when try to assign RequestOptions.proxy 
        /// to the in instance of HttpWebRequest and the behavior is match expected because 
        /// this unit test created only for verify if RequestOptions.Proxy assign to the 
        /// HttpWebRequest instance and do not care the value correct or not.
        /// 
        /// Go https://referencesource.microsoft.com/#System/net/System/Net/ServicePointManager.cs,33499ed90fd01409 
        /// to check method "internal static ServicePoint FindServicePoint(Uri address, IWebProxy proxy, out ProxyChain chain, ref HttpAbortDelegate abortDelegate, ref int abortState)"
        /// for detail (line 624 to line 626) if there are any concern.
        /// </summary>
        [Test()]
        [TestCase(WebRequestMethods.Http.Get)]
        [TestCase(WebRequestMethods.Http.Post)]
        public void GeneralTest_SetupProxyOnly_ThrowExpectedException(string httpVerb)
        {
            // arrange
            var stubPostConent = string.Empty;
            var stubUrl = $"http://{nameof(GeneralTest_SetupProxyOnly_ThrowExpectedException)}/{httpVerb}";
            var stubOptions = new RequestOptions();
            stubOptions.SetDefaultProxy();

            var mockRequest = SetupFakeRequest(stubUrl);

            var target = new HttpHelper();

            // act
            Action targetAction = null;
            if (httpVerb == WebRequestMethods.Http.Get)
            {
                targetAction = () => target.Get(stubUrl, stubOptions);
            }
            else if (httpVerb == WebRequestMethods.Http.Post)
            {
                targetAction = () => target.Post(stubUrl, stubPostConent, stubOptions);
            }

            // assert
            targetAction.Should().NotBeNull();
            targetAction.Should()
                        .Throw<ArgumentNullException>()
                        .WithMessage("Value cannot be null.*Parameter name: address");
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stubResponseString"></param>
        /// <param name="stubUrl">Must be defference everytime.</param>
        /// <returns></returns>
        private HttpWebRequest SetupFakeRequest(string stubUrl, string stubResponseString = "response")
        {
            var stubResponseStream = new MemoryStream(Encoding.UTF8.GetBytes(stubResponseString));
            var stubResponse = Substitute.For<HttpWebResponse>();
            stubResponse.GetResponseStream().Returns(stubResponseStream);

            var mockRequest = Substitute.ForPartsOf<HttpWebRequest>();
            mockRequest.When(r => r.GetResponse()).DoNotCallBase();
            mockRequest.GetResponse().Returns(stubResponse);
            mockRequest.When(r => r.GetRequestStream()).DoNotCallBase();
            mockRequest.GetRequestStream().Returns(new MemoryStream());
            mockRequest.Headers = new WebHeaderCollection();

            var stubRequestCreater = Substitute.For<IWebRequestCreate>();
            stubRequestCreater.Create(Arg.Any<Uri>()).Returns(mockRequest);
            WebRequest.RegisterPrefix(stubUrl, stubRequestCreater);

            return mockRequest;
        }
    }
}