using System;
using FluentAssertions;
using LazyGuy.Tests.Constants;
using NUnit.Framework;

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

        #region GetString

        [Test()]
        public void GetStringTest_DictionaryNull_ThrowArgumentNullExceptionWithMessage()
        {
            // Arragne
            string stubDictionary = null;
            var target = new RandomValueGenerator();
            int stubLength = 1;

            // Act
            Action act = () => { target.GetString(stubLength, stubDictionary); };

            // Assert
            act.Should().Throw<ArgumentNullException>().WithMessage(FakeMessageTemplates.ArgumentNull);
        }

        [Test()]
        public void GetStringTest_DictionaryEmpty_ThrowArgumentOutOfRangeExceptionWithMessage()
        {
            // Arragne
            string stubDictionary = string.Empty;
            var target = new RandomValueGenerator();
            int stubLength = 1;

            // Act
            Action act = () => { target.GetString(stubLength, stubDictionary); };

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>().WithMessage(FakeMessageTemplates.ArgumentEmpty);
        }
        #endregion

    }
}