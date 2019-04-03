using System;
using System.Security.Cryptography;
using LazyGuy.Constants;

namespace LazyGuy.Utils
{
    public class RandomValueGenerator
    {
        private RandomNumberGenerator _rng;

        public RandomValueGenerator(RandomNumberGenerator rng = null)
        {
            if (rng == null)
            {
                rng = new RNGCryptoServiceProvider();
            }

            _rng = rng;
        }

        public virtual string GetString(int length, string dictionary = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz")
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

        public virtual int GetInt(int min = 0, int max = int.MaxValue)
        {
            Argument.InRange(() => max >= min, nameof(max));
            
            if (min == max)
            {
                return min;
            }

            //use Int32 (4 bytes) bacause keyword 'int' is default as Int32
            var nextBytes = new byte[4];
            _rng.GetBytes(nextBytes);

            var range = (long)max - min;
            var shift = BitConverter.ToInt32(nextBytes, 0) % range;

            //shift always between int.MinValue and int.MaxValue, so it's safe convert to int directly
            if (shift < 0)
            {
                return max + (int)shift;
            }

            return min + (int)shift;
        }
    }
}
