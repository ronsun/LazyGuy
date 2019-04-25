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
        [TestCase(null)]
        [TestCase("")]
        public void NotNullOrEmptyTest_InputNullOrEmptyString_ThorwArgumentOutOfRangeExceptionWithMessage(string stubArg)
        {
            // arrange

            // act
            Action act = () => { Argument.NotNullOrEmpty(stubArg, nameof(stubArg)); };

            // assert
            act.Should().Throw<ArgumentOutOfRangeException>().WithMessage(MessageTemplates.ArgumentNullOrEmpty);
        }

        [Test()]
        public void NotNullOrEmptyTest_InputEmptyArray_ThorwArgumentOutOfRangeExceptionWithMessage()
        {
            // arrange
            string[] stubArg = new string[0];

            // act
            Action act = () => { Argument.NotNullOrEmpty(stubArg, nameof(stubArg)); };

            // assert
            act.Should().Throw<ArgumentOutOfRangeException>().WithMessage(MessageTemplates.ArgumentNullOrEmpty);
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