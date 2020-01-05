using System.Collections.Generic;
using System.Linq;

namespace LazyGuy.Extensions
{
    /// <summary>
    /// Extension methods for Dictionary
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Add an item to dictionary, update value if key exist.
        /// </summary>
        /// <typeparam name="TKey">Type of <paramref name="key"/>. </typeparam>
        /// <typeparam name="TValue">Type of <paramref name="value"/>. </typeparam>
        /// <param name="self"></param>
        /// <param name="key">Key of dictionary. </param>
        /// <param name="value">Value of dictionary. </param>
        public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key, TValue value)
        {
            AddSafty(self, key, value, true);
        }

        /// <summary>
        /// Add an item to dictionary, ignore if key exist.
        /// </summary>
        /// <typeparam name="TKey">Type of <paramref name="key"/>. </typeparam>
        /// <typeparam name="TValue">Type of <paramref name="value"/>. </typeparam>
        /// <param name="self"></param>
        /// <param name="key">Key of dictionary. </param>
        /// <param name="value">Value of dictionary. </param>
        public static void AddOrIgnore<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key, TValue value)
        {
            AddSafty(self, key, value, false);
        }

        private static void AddSafty<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key, TValue value, bool updateWhenDuplicate)
        {
            Argument.NotNull(self, nameof(key));
            Argument.NotNull(key, nameof(value));

            if (self.ContainsKey(key))
            {
                if (updateWhenDuplicate)
                {
                    self[key] = value;
                }
                return;
            }

            self.Add(key, value);
        }

        /// <summary>
        /// Convert Dictionary to query string.
        /// </summary>
        /// <param name="target"></param>
        /// <returns>
        ///     Return query string. Reutrn empty string if count of <paramref name="target"/> is 0
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="target"/> is null. 
        /// </exception>
        public static string ToQueryString(this IDictionary<string, string> target)
        {
            Argument.NotNull(target, nameof(target));

            if (target == null || target.Count == 0)
            {
                return string.Empty;
            }

            var result = target.Select(r => $"{r.Key}={r.Value}")
                               .Aggregate((left, right) => $"{left}&{right}");

            return result;
        }
    }
}
