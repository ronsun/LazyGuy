using FluentAssertions;
using LazyGuy.Tests.Constants;
using NUnit.Framework;
using System;

namespace LazyGuy.Utils.Tests
{
    [TestFixture()]
    public class RandomValueGeneratorTests_Nagative
    {
        #region GetInt

        [Test()]
        public void GetIntTest_MaxGreaterThanMin_ThrowArgumentOutOfRangeExceptionWithMessage()
        {
            // Arragne
            var target = new RandomValueGenerator();
            var stubMax = 0;
            var stubMin = 1;

            // Act
            Action act = () => { target.GetInt(stubMin, stubMax); };

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>().WithMessage(FakeMessageTemplates.NumberMustGreatherThanAnother);
        }

        #endregion


    }
}