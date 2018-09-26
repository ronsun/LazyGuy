using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace LazyGuy.Extensions.Tests
{
    [TestFixture()]
    public class DictionaryExtensionsTests_Negative
    {
        #region AddOrUpdate

        [Test()]
        public void AddOrUpdateTest_AddToNull_ThrowExpectedException()
        {
            //arrange
            Dictionary<string, string> target = null;
            string stubKey = "k";
            string stubValue = "v";

            //act
            Action act = () => { target.AddOrUpdate(stubKey, stubValue); };

            //assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Test()]
        public void AddOrUpdateTest_AddNullKey_ThrowExpectedException()
        {
            //arrange
            var target = new Dictionary<string, string>();
            string stubKey = null;
            string stubValue = "v";

            //act
            Action act = () => { target.AddOrUpdate(stubKey, stubValue); };

            //assert
            act.Should().Throw<ArgumentNullException>();
        }

        #endregion
    }
}