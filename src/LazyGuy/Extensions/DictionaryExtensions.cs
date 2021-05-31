using System.Collections.Generic;
using System.Linq;

namespace LazyGuy.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Dictionary{TKey, TValue}"/>.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Convert Dictionary to query string.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>
        ///     Return query string. Reutrn empty string if count of <paramref name="target"/> is 0.
        /// </returns>
        public static string ToQueryString(this IDictionary<string, string> target)
        {
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
