using NSubstitute;
using System;
using NUnit.Framework;

namespace LazyGuy.Tests
{
    [TestFixture()]
    public class ArgumentTests
    {
        #region NotNull

        [Test()]
        public void NotNullTest_InputNotNull_NoException()
        {
            // arrange
            string stubArg = string.Empty;

            // act
            Argument.NotNull(stubArg, nameof(stubArg));

            // assert
            Assert.Pass();
        }

        #endregion

        #region NotNullOrEmpty

        [Test()]
        public void NotNullOrEmptyTest_InputNotNullOrEmptyString_NoException()
        {
            // arrange
            string stubArg = "a";

            // act
            Argument.NotNullOrEmpty(stubArg, nameof(stubArg));

            // assert
            Assert.Pass();
        }

        [Test()]
        public void NotNullOrEmptyTest_InputNotNullOrEmptyArray_NoException()
        {
            // arrange
            string[] stubArg = new string[] { "a" };

            // act
            Argument.NotNullOrEmpty(stubArg, nameof(stubArg));

            // assert
            Assert.Pass();
        }

        #endregion

        #region EnumDefined

        private enum FakeEnum
        {
            A1
        }

        [Test()]
        public void EnumDefinedTest_InputDefinedEnum_NoException()
        {
            // arrange
            var stubEnum = FakeEnum.A1;

            // act
            Argument.EnumDefined(stubEnum, nameof(stubEnum));

            // assert
        }

        #endregion

        #region InRange

        [Test()]
        public void InRangeTest_InputFuncThatReturnTrue_FuncCalledSingleTimeWithoutException()
        {
            // arrange
            var stubArgName = "a";
            Func<bool> mockedFunc = Substitute.For<Func<bool>>();
            mockedFunc().Returns(true);

            // act
            Argument.InRange(mockedFunc, stubArgName);

            // assert
            mockedFunc.Received(1);
        }

        #endregion
    }
}