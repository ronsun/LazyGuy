using LazyGuy.Constants;
using System;
using System.Collections.Generic;

namespace LazyGuy.Extensions
{
    public static class DictionaryExtensions
    {
        public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key, TValue value)
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
                self[key] = value;
                return;
            }

            self.Add(key, value);
        }
    }
}
