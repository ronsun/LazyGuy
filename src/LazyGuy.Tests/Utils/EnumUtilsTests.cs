using FluentAssertions;
using NUnit.Framework;

namespace LazyGuy.Utils.Tests
{
    [TestFixture()]
    public class EnumUtilsTests
    {
        #region ParseDesciption

        private enum FakeEnum_ParseDesciption
        {
            [System.ComponentModel.Description("a")]
            WithDescription,
            [System.ComponentModel.Description("")]
            WithEmptyDescroption
        }

        [Test()]
        [TestCase("a", FakeEnum_ParseDesciption.WithDescription)]
        [TestCase("", FakeEnum_ParseDesciption.WithEmptyDescroption)]
        public void ParseDesciptionTest_InputDescription_ReturnExpectedEnum(
            string stubDescrioption,
            int expectedEnum)
        {
            // arrange
            
            // act
            var actual = EnumUtils.ParseDescription<FakeEnum_ParseDesciption>(stubDescrioption);

            // assert
            actual.Should().Be(expectedEnum);
        }

        #endregion

        #region TryParseDescription

        private enum FakeEnum_TryParseDescription
        {
            DefaultMember,
            [System.ComponentModel.Description("a")]
            WithDescription,
            [System.ComponentModel.Description("")]
            WithEmptyDescroption
        }

        [Test()]
        [TestCase("a", true, FakeEnum_TryParseDescription.WithDescription)]
        [TestCase("", true, FakeEnum_TryParseDescription.WithEmptyDescroption)]
        [TestCase(null, false, default(FakeEnum_TryParseDescription))]
        [TestCase("not exist", false, default(FakeEnum_TryParseDescription))]
        public void TryParseDescriptionTest_InputDescription_ReturnExpectedResultAndOutToExpectedEnum(
            string stubDescrioption,
            bool expectedResult,
            int expectedOutEnum)
        {
            // arrange

            // act
            FakeEnum_TryParseDescription actualOutEnum;
            var actualResult = EnumUtils.TryParseDescription(stubDescrioption, out actualOutEnum);

            // assert
            actualResult.Should().Be(expectedResult);
            actualOutEnum.Should().Be(expectedOutEnum);
        }

        #endregion
    }
}