using System;
using FluentAssertions;
using NUnit.Framework;

namespace LazyGuy.Utils.Tests
{
    [TestFixture()]
    public class RandomValueGeneratorTests_Exception
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
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        #endregion

        #region GetString

        [Test()]
        [TestCase(null)]
        [TestCase("")]
        public void GetStringTest_DictionaryNullOrEmpty_ThrowArgumentOutOfRangeExceptionWithMessage(string stubDictionary)
        {
            // Arragne
            var target = new RandomValueGenerator();
            int stubLength = 1;

            // Act
            Action act = () => { target.GetString(stubLength, stubDictionary); };

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        #endregion

    }
}