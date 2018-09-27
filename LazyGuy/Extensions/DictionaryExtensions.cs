using LazyGuy.Constants;
using System;
using System.Collections.Generic;

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
                throw new ArgumentNullException(ExceptionMessages.ArgumentNullMessage + nameof(self));
            }

            if (key == null)
            {
                throw new ArgumentNullException(ExceptionMessages.ArgumentNullMessage + nameof(key));
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
    }
}
