using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace LazyGuy.Utils.Tests
{
    [TestFixture()]
    public class RandomValueGeneratorTests
    {
        #region GetInt

        [Test()]
        public void GetIntTest_MinEqualsToMax_ReturnExpectedValue()
        {
            // arrange
            int stubMin = 0;
            int stubMax = 0;

            var target = new RandomValueGenerator();
            var expected = 0;

            // act
            int actual = target.GetInt(stubMin, stubMax);

            // assert
            actual.Should().Be(expected);
        }

        [Test()]
        public void GetIntTest_EdgeCase_ReturnExpectedValue()
        {
            // arrange
            int stubMin = int.MinValue;
            int stubMax = int.MaxValue;

            var stubRandomBytes = new byte[] { 1, 0, 0, 0 };
            var stubRNG = Substitute.For<RandomNumberGenerator>();

            FakeGetBytes(stubRNG, stubRandomBytes);

            var target = new RandomValueGenerator(stubRNG);
            var expected = stubMin + 1;

            // act
            int actual = target.GetInt(stubMin, stubMax);

            // assert
            actual.Should().Be(expected);
        }

        [Test()]
        [TestCaseSource(nameof(TestCase_GetIntTest_FixedMinAndMaxButDynamicBytes_ReturnExpectedValue))]
        public void GetIntTest_FixedMinAndMaxButDynamicBytes_ReturnExpectedValue(int stubMin, int stubMax, byte[] stubRandomBytes, int expected)
        {
            // arrange
            var stubRNG = Substitute.For<RandomNumberGenerator>();

            FakeGetBytes(stubRNG, stubRandomBytes);

            var target = new RandomValueGenerator(stubRNG);

            // act
            int actual = target.GetInt(stubMin, stubMax);

            // assert
            actual.Should().Be(expected);
        }

        private static List<object[]> TestCase_GetIntTest_FixedMinAndMaxButDynamicBytes_ReturnExpectedValue()
        {
            var cases = new List<object[]>()
            {
                // random 0, reutrn min
                new object[] { 1, 4, BitConverter.GetBytes(0), 1 },
                
                // random 1, return min + 1
                new object[] { 1, 4, BitConverter.GetBytes(1), 2 },
                
                // random 2, return min + 2
                new object[] { 1, 4, BitConverter.GetBytes(2), 3 },
                
                // random 3, return min
                new object[] { 1, 4, BitConverter.GetBytes(3), 1 },

                // random -1, return max - 1
                new object[] { 1, 4, BitConverter.GetBytes(-1), 3 },
                
                // random -2, return max - 2
                new object[] { 1, 4, BitConverter.GetBytes(-2), 2 },
                
                // random -3, return max - 3
                new object[] { 1, 4, BitConverter.GetBytes(-3), 1 },
                
                // random -4, return max - 1
                new object[] { 1, 4, BitConverter.GetBytes(-4), 3 },

                // as above, result include min but exclude max
            };
            return cases;
        }

        /// <summary>
        /// Restriction : (stubMaxGetButesValue - stubMinGetButesValue + 1) % (stubMax - stubMin) == 0, 
        /// ex: 32 % 16 == 0
        /// </summary>
        [Test()]
        public void GetIntTest_ReutrnValuesInSameRate()
        {
            // arrange
            int stubMin = 0;
            int stubMax = 16;
            int stubMinGetButesValue = -16;
            int stubMaxGetButesValue = 15;

            var stubRNG = Substitute.For<RandomNumberGenerator>();

            var expectedCountOfInt = stubMax - stubMin;
            var expectedHitTimes = 2;

            // act
            var actualDic = new Dictionary<int, int>();

            for (var i = stubMinGetButesValue; i <= stubMaxGetButesValue; i++)
            {
                FakeGetBytes(stubRNG, BitConverter.GetBytes(i));

                var target = new RandomValueGenerator(stubRNG);
                var actual = target.GetInt(stubMin, stubMax);
                if (actualDic.ContainsKey(actual))
                {
                    actualDic[actual]++;
                }
                else
                {
                    actualDic.Add(actual, 1);
                }
            }

            // assert
            actualDic.Should().HaveCount(expectedCountOfInt);
            actualDic.All(r => r.Value == expectedHitTimes).Should().BeTrue();
        }

        [Test()]
        public void GetIntTest_ThreadSafe()
        {
            //arrage
            int? firstThreadResult = null;
            var firstThread = new Thread(new ThreadStart(() =>
            {
                firstThreadResult = new RandomValueGenerator().GetInt();
            }));
            int? secondThreadResult = null;
            var secondThread = new Thread(new ThreadStart(() =>
            {
                secondThreadResult = new RandomValueGenerator().GetInt();
            }));

            //act
            firstThread.Start();
            secondThread.Start();
            firstThread.Join();
            secondThread.Join();

            //assert
            firstThreadResult.Should().NotBe(secondThreadResult);
        }

        #endregion

        #region GetString

        [Test()]
        public void GetStringTest_DefaultDictionary_ShouldBeExpected()
        {
            // arrange
            var stubRandomNumberGenerator = Substitute.For<RandomNumberGenerator>();
            var mockedRandomValueGenerator = Substitute.For<RandomValueGenerator>(stubRandomNumberGenerator);

            int stubLength = 1;
            var actualDictionary = string.Empty;
            mockedRandomValueGenerator
                .When(r => r.GetString(Arg.Any<int>(), Arg.Any<string>()))
                .Do(calledMethod =>
                {
                    actualDictionary = calledMethod.ArgAt<string>(1);
                });

            var expectedDefaultDictionary = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

            // act
            mockedRandomValueGenerator.GetString(stubLength);

            // assert
            actualDictionary.Should().Be(expectedDefaultDictionary);
        }

        [Test()]
        [TestCase(1, 0, "ab", "a")]
        [TestCase(2, 0, "ab", "aa")]
        [TestCase(1, 1, "ab", "b")]
        [TestCase(2, 1, "ab", "bb")]
        public void GetStringTest_CorrectArguments_ReturnExpectedString(
            int stubLength,
            int stubIndex,
            string stubDictionary,
            string expectedRandomString)
        {
            // arrange
            var stubRandomNumberGenerator = Substitute.For<RandomNumberGenerator>();
            var mockedRandomValueGenerator = Substitute.ForPartsOf<RandomValueGenerator>(stubRandomNumberGenerator);

            mockedRandomValueGenerator.GetInt(Arg.Any<int>(), Arg.Any<int>()).Returns(stubIndex);

            // act
            string actual = mockedRandomValueGenerator.GetString(stubLength, stubDictionary);

            // assert
            actual.Should().Be(expectedRandomString);
        }

        #endregion

        private void FakeGetBytes(RandomNumberGenerator fake, byte[] bytes)
        {
            fake.When(r => r.GetBytes(Arg.Is<byte[]>(s => s.Length == bytes.Length)))
                .Do(calledMethod =>
                {
                    var inputByteArray = calledMethod.Arg<byte[]>();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        inputByteArray[i] = bytes[i];
                    }
                });
        }
    }
}