using FluentAssertions;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

namespace LazyGuy.Extensions.Tests
{
    [TestFixture()]
    public class DictionaryExtensionsTests
    {
        #region AddOrUpdate

        [Test()]
        [TestCaseSource(nameof(TestCases_AddOrUpdateTest))]
        public void AddOrUpdateTest_AddToTarget_ExpectedResultAndCount(
            Dictionary<string, string> target,
            string stubKey,
            string stubValue,
            Dictionary<string, string> expected)
        {
            //arrange

            //act
            target.AddOrUpdate(stubKey, stubValue);

            //assert
            target.Count.Should().Be(expected.Count);
            target.Should().Equal(expected);

        }

        private static IEnumerable TestCases_AddOrUpdateTest()
        {
            string stubKey1 = "k1";
            string stubKey2 = "k2";

            string stubValue1 = "v1";
            string stubValue2 = "v2";

            // add one to empty dictionary
            yield return new TestCaseData(new Dictionary<string, string>(), stubKey1, stubValue1, new Dictionary<string, string> { [stubKey1] = stubValue1 });

            // add one to dictionary with different key
            yield return new TestCaseData(new Dictionary<string, string>() { [stubKey1] = stubValue1 }, stubKey2, stubValue2, new Dictionary<string, string> { [stubKey1] = stubValue1, [stubKey2] = stubValue2 });

            // add one to dictionary with same key
            yield return new TestCaseData(new Dictionary<string, string>() { [stubKey1] = stubValue1 }, stubKey1, stubValue2, new Dictionary<string, string> { [stubKey1] = stubValue2 });
        }

        #endregion

        #region AddOrIgnore

        [Test()]
        [TestCaseSource(nameof(TestCases_AddOrIgnoreTest))]
        public void AddOrIgnoreTest_AddToTarget_ExpectedResultAndCount(
            Dictionary<string, string> target,
            string stubKey,
            string stubValue,
            Dictionary<string, string> expected)
        {
            //arrange

            //act
            target.AddOrIgnore(stubKey, stubValue);

            //assert
            target.Count.Should().Be(expected.Count);
            target.Should().Equal(expected);
        }

        private static IEnumerable TestCases_AddOrIgnoreTest()
        {
            string stubKey1 = "k1";
            string stubKey2 = "k2";

            string stubValue1 = "v1";
            string stubValue2 = "v2";

            // add one to empty dictionary
            yield return new TestCaseData(new Dictionary<string, string>(), stubKey1, stubValue1, new Dictionary<string, string> { [stubKey1] = stubValue1 });

            // add one to dictionary with different key
            yield return new TestCaseData(new Dictionary<string, string>() { [stubKey1] = stubValue1 }, stubKey2, stubValue2, new Dictionary<string, string> { [stubKey1] = stubValue1, [stubKey2] = stubValue2 });

            // add one to dictionary with same key
            yield return new TestCaseData(new Dictionary<string, string>() { [stubKey1] = stubValue1 }, stubKey1, stubValue2, new Dictionary<string, string> { [stubKey1] = stubValue1 });
        }

        #endregion

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