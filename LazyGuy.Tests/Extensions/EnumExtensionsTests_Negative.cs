using FluentAssertions;
using LazyGuy.Tests.Constants;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace LazyGuy.Extensions.Tests
{
    [TestFixture()]
    public class EnumExtensionsTests_Negative
    {
        #region In

        /// <summary>
        /// Fake enum for test for In only,
        /// value must greather than 0 since smaller numbers are used for error case. 
        /// </summary>
        private enum FakeEnum_In_A
        {
            A1 = 1
        }

        /// <summary>
        /// Fake enum for test for In only, 
        /// value must greather than 0 since smaller numbers are used for error case. 
        /// </summary>
        private enum FakeEnum_In_B
        {
            B1 = 1
        }

        [Test()]
        [TestCaseSource(nameof(TestCases_InTest_InvalidEnumType))]
        public void InTest_InvalidEnumType_ThrowArrayTypeMismatchExceptionWithMessage(Enum target, Enum[] parameters, string expectedMessage)
        {
            // Arrange

            // Act
            Action act = () => { target.In(parameters); };

            // Assert
            act.Should().Throw<ArrayTypeMismatchException>().WithMessage(expectedMessage);
        }

        private static List<object[]> TestCases_InTest_InvalidEnumType()
        {
            var cases = new List<object[]>()
            {
                // type of target different
                new object[] { FakeEnum_In_A.A1, new Enum[] { FakeEnum_In_B.B1 }, FakeMessageTemplates.InvalidArrayTypeForParams },

                // one of types different in enum list
                new object[] { FakeEnum_In_B.B1, new Enum[] { FakeEnum_In_A.A1, FakeEnum_In_B.B1 }, FakeMessageTemplates.InvalidArrayTypeForParams },
            };
            return cases;
        }

        [Test()]
        [TestCaseSource(nameof(TestCases_InTest_InvalidEnumValue))]
        public void InTest_InvalidEnumValue_ThrowArgumentOutOfRangeExceptionWithMessage(Enum target, Enum[] parameters, string expectedMessage)
        {
            // Arrange

            // Act
            Action act = () => { target.In(parameters); };

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>().WithMessage(expectedMessage);
        }

        private static List<object[]> TestCases_InTest_InvalidEnumValue()
        {
            var cases = new List<object[]>()
            {
                // type of target out of range
                new object[] { (FakeEnum_In_A)(0), new Enum[] { FakeEnum_In_A.A1 }, FakeMessageTemplates.ValueNotInEnum },
                
                // type of parameter list out of range
                new object[] { FakeEnum_In_A.A1, new Enum[] { (FakeEnum_In_A)(0) } , FakeMessageTemplates.ValueNotInEnum },
            };
            return cases;
        }

        #endregion
    }
}