using LazyGuy.Constants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LazyGuy.Extensions
{
    public static class DictionaryExtensions
    {
        public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key, TValue value)
        {
            AddSafty(self, key, value, true);
        }

        public static void AddOrIgnore<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key, TValue value)
        {
            AddSafty(self, key, value, false);
        }

        private static void AddSafty<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key, TValue value, bool updateWhenDuplicate)
        {
            if (self == null)
            {
                string msg = string.Format(ExceptionMessages.ArgumentNullMessage, nameof(key));
                throw new ArgumentNullException(msg);
            }

            if (key == null)
            {
                string msg = string.Format(ExceptionMessages.ArgumentNullMessage, nameof(value));
                throw new ArgumentNullException(msg);
            }

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

        public static string ToQueryString(this IDictionary<string, string> target)
        {
            if (target == null)
            {
                string msg = string.Format(ExceptionMessages.ArgumentNullMessage, nameof(target));
                throw new ArgumentNullException(msg);
            }

            if (target.Count == 0)
            {
                return string.Empty;
            }

            var result = target.Select(r => $"{r.Key}={r.Value}")
                               .Aggregate((left, right) => $"{left}&{right}");

            return result;
        }
    }
}
