using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace LazyGuy.Utils.Tests
{
    [TestFixture()]
    public class RandomValueGeneratorTests_Positive
    {
        #region GetInt

        [Test()]
        public void GetIntTest_MixEqualsToMax_ReturnExpectedValue()
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
        public void GetIntTest_InputExtremum_ReturnExpectedValue()
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

        static List<object[]> TestCase_GetIntTest_FixedMinAndMaxButDynamicBytes_ReturnExpectedValue()
        {
            var cases = new List<object[]>()
            {
                // random 0, reutrn min
                new object[] { 1, 4, new byte[] { 0, 0, 0, 0 }, 1 },
                
                // random 1, return min + 1
                new object[] { 1, 4, new byte[] { 1, 0, 0, 0 }, 2 },
                
                // random 2, return min + 2
                new object[] { 1, 4, new byte[] { 2, 0, 0, 0 }, 3 },
                
                // random 3, return min
                new object[] { 1, 4, new byte[] { 3, 0, 0, 0 }, 1 },

                // random -1, return max - 1
                new object[] { 1, 4, new byte[] { 255, 255, 255, 255 }, 3 },
                
                // random -2, return max - 2
                new object[] { 1, 4, new byte[] { 254, 255, 255, 255 }, 2 },
                
                // random -3, return max - 3
                new object[] { 1, 4, new byte[] { 253, 255, 255, 255 }, 1 },
                
                // random -4, return min
                new object[] { 1, 4, new byte[] { 253, 255, 255, 255 }, 1 },

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