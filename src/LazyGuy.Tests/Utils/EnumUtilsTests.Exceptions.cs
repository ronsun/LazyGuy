using System;
using FluentAssertions;
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
            var stubDescrioption = "not exist";

            // Act
            Action act = () => { EnumUtils.ParseDescription<FakeEnum_ParseDesciption>(stubDescrioption); };

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Test()]
        public void ParseDesciptionTest_ParseToEmptyEnum_ThrowArgumentOutOfRangeExceptionWithMessage()
        {
            // arrange
            var stubDescrioption = "a";

            // Act
            Action act = () => { EnumUtils.ParseDescription<FakeEnum_ParseDesciption_Empty>(stubDescrioption); };

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Test()]
        public void ParseDesciptionTest_InputNull_ThrowNullReferenceExceptionWithMessage()
        {
            // arrange
            string stubDescrioption = null;

            // Act
            Action act = () => { EnumUtils.ParseDescription<FakeEnum_ParseDesciption>(stubDescrioption); };

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        #endregion
    }
}