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

        private enum FakeEnum_ParseDesciption_Empty
        {

        }

        [Test()]
        public void ParseDesciptionTest_InputDescriptionNotExist_ThrowArgumentOutOfRangeExceptionWithMessage()
        {
            // arrange
            var stubDescrioption = "b";

            // Act
            Action act = () => { EnumUtils.ParseDescription<FakeEnum_ParseDesciption>(stubDescrioption); };

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>().WithMessage(MessageTemplates.ArgumentOutOfRange);
        }

        [Test()]
        public void ParseDesciptionTest_ParseToEmptyEnum_ThrowArgumentOutOfRangeExceptionWithMessage()
        {
            // arrange
            var stubDescrioption = "a";

            // Act
            Action act = () => { EnumUtils.ParseDescription<FakeEnum_ParseDesciption>(stubDescrioption); };

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
            Action act = () => { EnumUtils.ParseDescription<FakeEnum_ParseDesciption>(stubDescrioption); };

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>().WithMessage(MessageTemplates.ArgumentNullOrEmpty);
        }

        #endregion
    }
}