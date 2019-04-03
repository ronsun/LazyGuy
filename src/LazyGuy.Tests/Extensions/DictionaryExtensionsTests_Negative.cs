using System;
using System.Collections.Generic;
using FluentAssertions;
using LazyGuy.Tests.Constants;
using NUnit.Framework;

namespace LazyGuy.Extensions.Tests
{
    [TestFixture()]
    public class DictionaryExtensionsTests_Negative
    {
        #region AddOrUpdate

        [Test()]
        public void AddOrUpdateTest_AddToNull_ThrowArgumentNullExceptionWithMessage()
        {
            //arrange
            Dictionary<string, string> target = null;
            string stubKey = "k";
            string stubValue = "v";

            //act
            Action act = () => { target.AddOrUpdate(stubKey, stubValue); };

            //assert
            act.Should().Throw<ArgumentNullException>().WithMessage(MessageTemplates.ArgumentNull);
        }

        [Test()]
        public void AddOrUpdateTest_AddNullKey_ThrowArgumentNullExceptionWithMessage()
        {
            //arrange
            var target = new Dictionary<string, string>();
            string stubKey = null;
            string stubValue = "v";

            //act
            Action act = () => { target.AddOrUpdate(stubKey, stubValue); };

            //assert
            act.Should().Throw<ArgumentNullException>().WithMessage(MessageTemplates.ArgumentNull);
        }

        #endregion

        #region ToQueryString

        [Test()]
        public void ToQueryStringTest_NullTarget_ThrowArgumentNullExceptionWithMessage()
        {
            //arrange
            Dictionary<string, string> target = null;

            //act
            Action act = () => { target.ToQueryString(); };

            //assert
            act.Should().Throw<ArgumentNullException>().WithMessage(MessageTemplates.ArgumentNull);
        }

        #endregion
    }
}