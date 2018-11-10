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
        #region Get

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
            var actual = target.CurrentRequest;

            // assert
            actual.Timeout.Should().Be(expectedTimeout);
            actual.ReadWriteTimeout.Should().Be(expectedReadWriteTimeout);
            actual.ContentType.Should().Be(expectedContentType);
            actual.Method.Should().Be(expectedMethod);
            actual.Proxy.Should().BeEquivalentTo(expectedProxy);
        }

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

        [Test()]
        public void GetTest_SetupOptionsWithoutProxy_ParametersOfRequestMatchExpected()
        {
            // arrange
            var stubResponseString = "response";
            var stubUrl = $"http://{nameof(GetTest_SetupOptionsWithoutProxy_ParametersOfRequestMatchExpected)}";
            var stubOptions = new RequestOptions()
            {
                Encoding = Encoding.Default,
                Timeout = 1,
                ReadWriteTimeout = 2,
                ContentType = "contentType"
            };

            var mockRequest = SetupFakeRequest(stubResponseString, stubUrl);

            var target = new HttpHelper();

            string expectedMethod = "GET";
            int expectedTimeout = stubOptions.Timeout;
            int expectedReadWriteTimeout = stubOptions.ReadWriteTimeout;
            string expectedContentType = stubOptions.ContentType;

            // act
            target.Get(stubUrl, stubOptions);
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
        public void GetTest_SetupProxyOnly_ThrowExpectedException()
        {
            // arrange
            var stubResponseString = "response";
            var stubUrl = $"http://{nameof(GetTest_SetupProxyOnly_ThrowExpectedException)}";
            var stubOptions = new RequestOptions();
            stubOptions.SetDefaultProxy();

            var mockRequest = SetupFakeRequest(stubResponseString, stubUrl);

            var target = new HttpHelper();

            // act
            Action targetAction = () => target.Get(stubUrl, stubOptions);

            // assert
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