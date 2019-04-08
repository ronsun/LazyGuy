using System;
using FluentAssertions;
using LazyGuy.Tests.Constants;
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
            act.Should().Throw<ArgumentNullException>().WithMessage(MessageTemplates.ArgumentNull);
        }

        #endregion

        #region NotNullOrEmpty

        [Test()]
        public void NotNullOrEmptyTest_InputNull_ThorwArgumentNullExceptionWithMessage()
        {
            // arrange
            string stubArg = null;

            // act
            Action act = () => { Argument.NotNullOrEmpty(stubArg, nameof(stubArg)); };

            // assert
            act.Should().Throw<ArgumentNullException>().WithMessage(MessageTemplates.ArgumentNull);
        }

        [Test()]
        public void NotNullOrEmptyTest_InputEmptyString_ThorwArgumentOutOfRangeExceptionWithMessage()
        {
            // arrange
            string stubArg = string.Empty;

            // act
            Action act = () => { Argument.NotNullOrEmpty(stubArg, nameof(stubArg)); };

            // assert
            act.Should().Throw<ArgumentOutOfRangeException>().WithMessage(MessageTemplates.ArgumentEmpty);
        }

        [Test()]
        public void NotNullOrEmptyTest_InputEmptyArray_ThorwArgumentOutOfRangeExceptionWithMessage()
        {
            // arrange
            string[] stubArg = new string[0];

            // act
            Action act = () => { Argument.NotNullOrEmpty(stubArg, nameof(stubArg)); };

            // assert
            act.Should().Throw<ArgumentOutOfRangeException>().WithMessage(MessageTemplates.ArgumentEmpty);
        }

        #endregion

        #region EnumDefined

        private enum FakeEnum
        {
            A1
        }

        [Test()]
        public void EnumDefinedTest_InputUndefinedEnum_ThorwArgumentOutOfRangeExceptionWithMessage()
        {
            // arrange
            var stubEnum = (FakeEnum)1;

            // act
            Action act = () => { Argument.EnumDefined(stubEnum, nameof(stubEnum)); };

            // assert
            act.Should().Throw<ArgumentOutOfRangeException>().WithMessage(MessageTemplates.ValueNotInEnum);
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
            act.Should().Throw<ArgumentOutOfRangeException>().WithMessage(MessageTemplates.ArgumentOutOfRange);
        }

        [Test()]
        public void InRangeTest_InputFuncThatReturnFalseAndCustomizeMessage_ThorwArgumentOutOfRangeExceptionWithMessage()
        {
            // arrange
            var stubArgName = "a";
            var stubMessage = "m";
            Func<bool> stubFunc = () => false;

            // act
            Action act = () => { Argument.InRange(stubFunc, stubArgName, stubMessage); };

            // assert
            act.Should().Throw<ArgumentOutOfRangeException>().WithMessage(stubMessage);
        }
        #endregion
    }
}