using System;
using System.Security.Cryptography;

namespace LazyGuy.Utils
{
    public class RandomValueGenerator
    {
        private RandomNumberGenerator _rng;

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomValueGenerator"/> class.
        /// </summary>
        public RandomValueGenerator()
            : this(new RNGCryptoServiceProvider())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomValueGenerator"/> class.
        /// </summary>
        /// <param name="rng">Dependent <see cref="RandomValueGenerator"/>. </param>
        public RandomValueGenerator(RandomNumberGenerator rng)
        {
            _rng = rng;
        }

        /// <summary>
        /// Get random string, the characters in random string should in "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".
        /// </summary>
        /// <param name="length">String length.</param>
        /// <returns>Random string.</returns>
        public virtual string GetString(int length)
        {
            return GetString(length, "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz");
        }

        /// <summary>
        /// Get random string.
        /// </summary>
        /// <param name="length">String length.</param>
        /// <param name="dictionary">Characters for random, ex: if be "abc", then the all characters in random string should be 'a' or 'b' or 'c'.</param>
        /// <returns>Random string.</returns>
        public virtual string GetString(int length, string dictionary)
        {
            Argument.NotNullOrEmpty(dictionary, nameof(dictionary));

            var charArray = new char[length];
            for (int i = 0; i < length; i++)
            {
                var index = GetInt(0, dictionary.Length);
                charArray[i] = dictionary[index];
            }

            return new string(charArray);
        }

        /// <summary>
        /// Get random int between <see cref="int.MinValue"/> and <see cref="int.MaxValue"/>.
        /// </summary>
        /// <returns>Random int.</returns>
        public virtual int GetInt()
        {
            return GetInt(int.MinValue, int.MaxValue);
        }

        /// <summary>
        /// Get random int in range, include min but exclude max.
        /// </summary>
        /// <param name="min">Minimum value of range, default: 0.</param>
        /// <param name="max">Maximun valud of range, defalut: int.MaxValue. </param>
        /// <returns>Random int.</returns>
        public virtual int GetInt(int min, int max)
        {
            Argument.InRange(() => max >= min, nameof(max));

            if (min == max)
            {
                return min;
            }

            // use Int32 (4 bytes) bacause keyword 'int' is default as Int32
            var nextBytes = new byte[4];
            _rng.GetBytes(nextBytes);

            var range = (long)max - min;
            var shift = BitConverter.ToInt32(nextBytes, 0) % range;

            // shift always between int.MinValue and int.MaxValue, so it's safe convert to int directly
            if (shift < 0)
            {
                return max + (int)shift;
            }

            return min + (int)shift;
        }
    }
}
