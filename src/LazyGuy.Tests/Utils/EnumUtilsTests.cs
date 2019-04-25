using NUnit.Framework;
using LazyGuy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace LazyGuy.Utils.Tests
{
    [TestFixture()]
    public class EnumUtilsTests
    {
        #region ParseDesciption

        private enum FakeEnum_ParseDesciption
        {
            [System.ComponentModel.Description("a")]
            WithDescription
        }

        [Test()]
        public void ParseDesciptionTest_InputExistingDescription_ReturnExpectedEnum()
        {
            // arrange
            var stubDescrioption = "a";
            var expectedEnum = FakeEnum_ParseDesciption.WithDescription;

            // act
            var actual = EnumUtils.ParseDesciption<FakeEnum_ParseDesciption>(stubDescrioption);

            // assert
            actual.Should().Be(expectedEnum);
        }

        #endregion
    }
}