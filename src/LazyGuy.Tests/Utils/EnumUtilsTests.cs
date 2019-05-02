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
        public void ParseDesciptionTest_InputExistingDescription_ReturnExpectedEnum()
        {
            // arrange
            var stubDescrioption = "a";
            var expectedEnum = FakeEnum_ParseDesciption.WithDescription;

            // act
            var actual = EnumUtils.ParseDescription<FakeEnum_ParseDesciption>(stubDescrioption);

            // assert
            actual.Should().Be(expectedEnum);
        }

        [Test()]
        public void ParseDesciptionTest_InputEmptyDescription_ReturnExpectedEnum()
        {
            // arrange
            var stubDescrioption = "";
            var expectedEnum = FakeEnum_ParseDesciption.WithEmptyDescroption;

            // act
            var actual = EnumUtils.ParseDescription<FakeEnum_ParseDesciption>(stubDescrioption);

            // assert
            actual.Should().Be(expectedEnum);
        }

        #endregion
    }
}