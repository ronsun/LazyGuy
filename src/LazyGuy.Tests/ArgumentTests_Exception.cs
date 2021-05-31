using System;
using FluentAssertions;
using NUnit.Framework;

namespace LazyGuy.Tests
{
    [TestFixture()]
    public class ArgumentTests_Exception
    {
        #region NotNull

        [Test()]
        public void NotNullTest_InputNull_ThorwArgumentNullExceptionWithMessage()
        {
            // arrange
            string stubArg = null;

            // act
            Action act = () => { Argument.NotNull(stubArg, nameof(stubArg)); };

            // assert
            act.Should().Throw<ArgumentNullException>();
        }

        #endregion

        #region EnumDefined

        private enum FakeEnum_EnumDefined
        {
            A1
        }

        [Test()]
        public void EnumDefinedTest_InputUndefinedEnum_ThorwArgumentOutOfRangeExceptionWithMessage()
        {
            // arrange
            var stubEnum = (FakeEnum_EnumDefined)1;

            // act
            Action act = () => { Argument.EnumDefined(stubEnum, nameof(stubEnum)); };

            // assert
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        #endregion

        #region InRange

        [Test()]
        public void InRangeTest_InputFuncThatReturnFalse_ThorwArgumentOutOfRangeExceptionWithMessage()
        {
            // arrange
            var stubArgName = "a";
            Func<bool> stubFunc = () => false;

            // act
            Action act = () => { Argument.InRange(stubFunc, stubArgName); };

            // assert
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        #endregion
    }
}