using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;

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

            var stubRandomBytes = BitConverter.GetBytes(1);
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

        private static IEnumerable TestCase_GetIntTest_FixedMinAndMaxButDynamicBytes_ReturnExpectedValue()
        {
            // For positive random value N, let 0 <= N < 4, return min + N
            yield return new TestCaseData(1, 4, BitConverter.GetBytes(0), 1);
            yield return new TestCaseData(1, 4, BitConverter.GetBytes(1), 2);
            yield return new TestCaseData(1, 4, BitConverter.GetBytes(2), 3);
            yield return new TestCaseData(1, 4, BitConverter.GetBytes(3), 1);

            // For negative random value N, let -1 <= N < 0, return max + N
            yield return new TestCaseData(1, 4, BitConverter.GetBytes(-1), 3);
            yield return new TestCaseData(1, 4, BitConverter.GetBytes(-2), 2);
            yield return new TestCaseData(1, 4, BitConverter.GetBytes(-3), 1);
            yield return new TestCaseData(1, 4, BitConverter.GetBytes(-4), 3);
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
        public void GetStringTest_InputLength_ShouldBeExpected()
        {
            // arrange
            var stubRandomNumberGenerator = Substitute.For<RandomNumberGenerator>();
            var mockedRandomValueGenerator = Substitute.For<RandomValueGenerator>(stubRandomNumberGenerator);
            var stubDictionary = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

            int stubLength = 1;
            var actualDictionary = string.Empty;
            mockedRandomValueGenerator
                .When(r => r.GetString(Arg.Any<int>()))
                .Do(calledMethod =>
                {
                    actualDictionary = stubDictionary;
                });

            var expectedDefaultDictionary = stubDictionary;

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
        public void GetStringTest_InputLengthAndDictionary_ReturnExpectedString(
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

        /// <summary>
        /// Always use stub value when RandomNumberGenerator.GetBytes() called.
        /// </summary>
        /// <param name="fake">RandomNumberGenerator.</param>
        /// <param name="stubBytes">stub byte array.</param>
        private void FakeGetBytes(RandomNumberGenerator fake, byte[] stubBytes)
        {
            fake.When(r => r.GetBytes(Arg.Is<byte[]>(s => s.Length == stubBytes.Length)))
                .Do(calledMethod =>
                {
                    var inputByteArray = calledMethod.Arg<byte[]>();
                    for (int i = 0; i < stubBytes.Length; i++)
                    {
                        inputByteArray[i] = stubBytes[i];
                    }
                });
        }
    }
}