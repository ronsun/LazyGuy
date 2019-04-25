using System;
using FluentAssertions;
using LazyGuy.Tests.Constants;
using NUnit.Framework;

namespace LazyGuy.Utils.Tests
{
    [TestFixture()]
    public class EnumUtilsTests_Exception
    {
        #region ParseDesciption

        private enum FakeEnum_ParseDesciption
        {
            [System.ComponentModel.Description("a")]
            WithDescription
        }

        [Test()]
        public void ParseDesciptionTest_InputDescriptionNotExist_ThrowArgumentOutOfRangeExceptionWithMessage()
        {
            // arrange
            var stubDescrioption = "b";

            // Act
            Action act = () => { EnumUtils.ParseDesciption<FakeEnum_ParseDesciption>(stubDescrioption); };

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>().WithMessage(MessageTemplates.ArgumentOutOfRange);
        }

        [Test()]
        [TestCase(null)]
        [TestCase("")]
        public void ParseDesciptionTest_InputNullOrEmpty_ThrowArgumentOutOfRangeExceptionWithMessage(string stubDescrioption)
        {
            // arrange

            // Act
            Action act = () => { EnumUtils.ParseDesciption<FakeEnum_ParseDesciption>(stubDescrioption); };

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>().WithMessage(MessageTemplates.ArgumentNullOrEmpty);
        }

        #endregion
    }
}