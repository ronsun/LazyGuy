using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace LazyGuy.Extensions.Tests
{
    [TestFixture()]
    public class DictionaryExtensionsTests_Positive
    {
        #region AddOrUpdate

        [Test()]
        [TestCaseSource(nameof(AddOrUpdate_SuccessCases))]
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

        private static List<object[]> AddOrUpdate_SuccessCases()
        {
            string stubKey1 = "k1";
            string stubKey2 = "k2";

            string stubValue1 = "v1";
            string stubValue2 = "v2";

            var cases = new List<object[]>()
            {
                // add one to empty dictionary
                new object[]{ new Dictionary<string, string>(), stubKey1, stubValue1, new Dictionary<string, string> { [stubKey1] = stubValue1 } },

                // add one to dictionary with different key
                new object[]{ new Dictionary<string, string>() { [stubKey1] = stubValue1 }, stubKey2, stubValue2, new Dictionary<string, string> { [stubKey1] = stubValue1, [stubKey2] = stubValue2 } },

                // add one to dictionary with same key
                new object[]{ new Dictionary<string, string>() { [stubKey1] = stubValue1 }, stubKey1, stubValue2, new Dictionary<string, string> { [stubKey1] = stubValue2 } },
            };
            return cases;
        }

        #endregion

        #region AddOrIgnore

        [Test()]
        [TestCaseSource(nameof(AddOrIgnore_SuccessCases))]
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

        private static List<object[]> AddOrIgnore_SuccessCases()
        {
            string stubKey1 = "k1";
            string stubKey2 = "k2";

            string stubValue1 = "v1";
            string stubValue2 = "v2";

            var cases = new List<object[]>()
            {
                // add one to empty dictionary
                new object[]{ new Dictionary<string, string>(), stubKey1, stubValue1, new Dictionary<string, string> { [stubKey1] = stubValue1 } },

                // add one to dictionary with different key
                new object[]{ new Dictionary<string, string>() { [stubKey1] = stubValue1 }, stubKey2, stubValue2, new Dictionary<string, string> { [stubKey1] = stubValue1, [stubKey2] = stubValue2 } },

                // add one to dictionary with same key
                new object[]{ new Dictionary<string, string>() { [stubKey1] = stubValue1 }, stubKey1, stubValue2, new Dictionary<string, string> { [stubKey1] = stubValue1 } },
            };
            return cases;
        }

        #endregion

        #region ToQueryString

        [Test()]
        [TestCaseSource(nameof(ToQueryString_SuccessCases))]
        public void ToQueryStringTest_ValidTarget_GetExpectedString(Dictionary<string, string> target, string expected)
        {
            //arrange

            //act
            string actual = target.ToQueryString();

            //assert
            actual.Should().Be(expected);
        }

        private static List<object[]> ToQueryString_SuccessCases()
        {
            string stubKey1 = "k1";
            string stubKey2 = "k2";

            string stubValue1 = "v1";
            string stubValue2 = "v2";

            var cases = new List<object[]>()
            {
                // empty to query string
                new object[]{ new Dictionary<string, string>(), string.Empty },

                // dictionary with single item to query string
                new object[]{ new Dictionary<string, string>() { [stubKey1] = stubValue1 }, $"{stubKey1}={stubValue1}" },

                // dictionary with multiple item to query string 
                new object[]{ new Dictionary<string, string>() { [stubKey1] = stubValue1, [stubKey2] = stubValue2 }, $"{stubKey1}={stubValue1}&{stubKey2}={stubValue2}" },
            };
            return cases;
        }

        #endregion
    }
}