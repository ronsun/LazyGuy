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
        public void AddOrUpdateTest_AddToEmpty_ExpectedResultAndCount()
        {
            //arrange
            string stubKey = "k";
            string stubValue = "v";

            string expectedKey = stubKey;
            string expectedValue = stubValue;

            var target = new Dictionary<string, string>();

            //act
            target.AddOrUpdate(stubKey, stubValue);

            //assert
            target.Should().HaveCount(1);
            target.Should().Contain(expectedKey, expectedValue);
        }

        [Test()]
        public void AddOrUpdateTest_AddWithoutDuplicate_ExpectedResultAndCount()
        {
            //arrange
            string stubKey1 = "k1";
            string stubValue1 = "v1";
            string stubKey2 = "k2";
            string stubValue2 = "v2";

            string expectedKey1 = stubKey1;
            string expectedValue1 = stubValue1;
            string expectedKey2 = stubKey2;
            string expectedValue2 = stubValue2;

            var target = new Dictionary<string, string>() { [stubKey1] = stubValue1 };

            //act
            target.AddOrUpdate(stubKey2, stubValue2);

            //assert
            target.Should().HaveCount(2);
            target.Should().Contain(expectedKey1, expectedValue1);
            target.Should().Contain(expectedKey2, expectedValue2);
        }

        #endregion
    }
}