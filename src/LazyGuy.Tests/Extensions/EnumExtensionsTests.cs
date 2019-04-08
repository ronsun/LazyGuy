using FluentAssertions;
using NUnit.Framework;

namespace LazyGuy.Extensions.Tests
{
    [TestFixture()]
    public class EnumExtensionsTests
    {
        #region In

        /// <summary>
        /// Fake enum for test for In only
        /// </summary>
        private enum FakeEnum_In_A
        {
            A1 = 1,
            A2 = 2,
            A3 = 3
        }

        [Test()]
        public void InTest_EnumNotInParames_ReturnFalse()
        {
            // arrange
            var target = FakeEnum_In_A.A1;

            // act
            bool actual = target.In(FakeEnum_In_A.A2, FakeEnum_In_A.A3);

            // assert
            actual.Should().BeFalse();
        }

        [Test()]
        public void InTest_EnumInParames_ReturnTrue()
        {
            // arrange
            var target = FakeEnum_In_A.A2;

            // act
            bool actual = target.In(FakeEnum_In_A.A1, FakeEnum_In_A.A2, FakeEnum_In_A.A3);

            // assert
            actual.Should().BeTrue();
        }

        [Test()]
        public void InTest_EmptyParames_ReturnFalse()
        {
            // arrange
            var target = FakeEnum_In_A.A3;

            // act
            bool actual = target.In();

            // assert
            actual.Should().BeFalse();
        }
        #endregion
    }
}