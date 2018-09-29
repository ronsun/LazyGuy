using FluentAssertions;
using LazyGuy.Constants;
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
        [TestCaseSource(nameof(In_ArrayTypeMismatchExceptionCases))]
        public void InTest_InvalidInput_ThrowArrayTypeMismatchExceptionContainsMessage(Enum target, Enum[] parameters, string containedErrorMessage)
        {
            // Arrange

            // Act
            Action act = () => { target.In(parameters); };

            // Assert
            act.Should().Throw<ArrayTypeMismatchException>().Where(e => e.Message.Contains(containedErrorMessage));
        }

        private static List<object[]> In_ArrayTypeMismatchExceptionCases()
        {
            var cases = new List<object[]>()
            {
                // type of target different
                new object[] { FakeEnum_In_A.A1, new Enum[] { FakeEnum_In_B.B1 }, ExceptionMessages.InvalidArrayTypeForParams },

                // one of types different in enum list
                new object[] { FakeEnum_In_B.B1, new Enum[] { FakeEnum_In_A.A1, FakeEnum_In_B.B1 }, ExceptionMessages.InvalidArrayTypeForParams },
            };
            return cases;
        }

        [Test()]
        [TestCaseSource(nameof(In_ArgumentOutOfRangeExceptionCases))]
        public void InTest_InvalidInput_ThrowArgumentOutOfRangeException(Enum target, Enum[] parameters)
        {
            // Arrange

            // Act
            Action act = () => { target.In(parameters); };

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        private static List<object[]> In_ArgumentOutOfRangeExceptionCases()
        {
            var cases = new List<object[]>()
            {
                // type of target out of range
                new object[] { (FakeEnum_In_A)(0), new Enum[] { FakeEnum_In_A.A1 }  },
                
                // type of parameter list out of range
                new object[] { FakeEnum_In_A.A1, new Enum[] { (FakeEnum_In_A)(0) }  },
            };
            return cases;
        }

        #endregion
    }
}