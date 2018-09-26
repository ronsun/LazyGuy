using NUnit.Framework;
using LazyGuy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace LazyGuy.Tests
{
    [TestFixture()]
    public class InitTests
    {
        [Test()]
        public void EchoTest_InputBool_Echo()
        {
            // arrange
            bool stuckBool = true;

            // act
            var actual = new Init().Echo<bool>(stuckBool);

            // assert
            actual.Should().BeTrue();
        }


        [Test()]
        public void EchoTest_InputString_Echo()
        {
            // arrange
            string stuckString = "a";
            string expected = stuckString;

            // act
            var actual = new Init().Echo<string>(stuckString);

            // assert
            actual.Should().Be(expected);
        }

    }
}