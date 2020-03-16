using FluentAssertions;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

namespace LazyGuy.Extensions.Tests
{
    [TestFixture()]
    public class DictionaryExtensionsTests
    {
        #region ToQueryString

        [Test()]
        [TestCaseSource(nameof(TestCases_ToQueryStringTest))]
        public void ToQueryStringTest_ValidTarget_GetExpectedString(Dictionary<string, string> target, string expected)
        {
            //arrange

            //act
            string actual = target.ToQueryString();

            //assert
            actual.Should().Be(expected);
        }

        private static IEnumerable TestCases_ToQueryStringTest()
        {
            string stubKey1 = "k1";
            string stubKey2 = "k2";

            string stubValue1 = "v1";
            string stubValue2 = "v2";

            // empty to query string
            yield return new TestCaseData(new Dictionary<string, string>(), string.Empty);

            // dictionary with single item to query string
            yield return new TestCaseData(new Dictionary<string, string>() { [stubKey1] = stubValue1 }, $"{stubKey1}={stubValue1}");

            // dictionary with multiple item to query string 
            yield return new TestCaseData(new Dictionary<string, string>() { [stubKey1] = stubValue1, [stubKey2] = stubValue2 }, $"{stubKey1}={stubValue1}&{stubKey2}={stubValue2}");
        }

        #endregion
    }
}