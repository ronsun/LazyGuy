﻿using FluentAssertions;
using LazyGuy.Tests.Constants;
using NUnit.Framework;
using System;
using System.Collections;

namespace LazyGuy.Extensions.Tests
{
    [TestFixture()]
    public class EnumExtensionsTests_Exception
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

        private static IEnumerable TestCases_InTest_InvalidEnumType()
        {
            // different type of target 
            yield return new TestCaseData(FakeEnum_In_A.A1, new Enum[] { FakeEnum_In_B.B1 }, MessageTemplates.InvalidArrayTypeForParams);

            // one of types different in enum list
            yield return new TestCaseData(FakeEnum_In_B.B1, new Enum[] { FakeEnum_In_A.A1, FakeEnum_In_B.B1 }, MessageTemplates.InvalidArrayTypeForParams);
        }

        [Test()]
        [TestCaseSource(nameof(TestCases_InTest_InvalidEnumValue))]
        public void InTest_InvalidEnumValue_ThrowArgumentOutOfRangeExceptionWithMessage(Enum target, Enum[] parameters)
        {
            // Arrange

            // Act
            Action act = () => { target.In(parameters); };

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        private static IEnumerable TestCases_InTest_InvalidEnumValue()
        {
            // type of target out of range
            yield return new TestCaseData((FakeEnum_In_A)(0), new Enum[] { FakeEnum_In_A.A1 });

            // type of parameter list out of range
            yield return new TestCaseData(FakeEnum_In_A.A1, new Enum[] { (FakeEnum_In_A)(0) });
        }

        #endregion
    }
}