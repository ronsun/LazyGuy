using System;
using System.Collections.Generic;
using FluentAssertions;
using LazyGuy.Tests.Constants;
using NUnit.Framework;

namespace LazyGuy.Extensions.Tests
{
    [TestFixture()]
    public class DictionaryExtensionsTests_Exception
    {
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