using System;
using System.ComponentModel;

namespace LazyGuy.Utils
{
    /// <summary>
    /// Enum utils.
    /// </summary>
    public static class EnumUtils
    {
        /// <summary>
        /// Parse from description of <see cref="DescriptionAttribute"/> to enum.
        /// </summary>
        /// <typeparam name="T">Enum type.</typeparam>
        /// <param name="description">Description.</param>
        /// <returns>Parsed enum type.</returns>
        public static T ParseDescription<T>(string description)
            where T : Enum
        {
            string enumFieldName = FindFieldName<T>(description);
            T result = (T)Enum.Parse(typeof(T), enumFieldName);
            return result;
        }

        /// <summary>
        /// Try from parse from description of <see cref="DescriptionAttribute"/> to enum.
        /// </summary>
        /// <typeparam name="T">Enum type.</typeparam>
        /// <param name="description">Description.</param>
        /// <param name="result">Parsed result.</param>
        /// <returns>Whether parsed successfully.</returns>
        public static bool TryParseDescription<T>(string description, out T result)
            where T : struct, Enum
        {
            result = default;

            if (description == null)
            {
                return false;
            }

            string enumFieldName = FindFieldName<T>(description);
            if (enumFieldName == null)
            {
                return false;
            }

            bool isSuccess = Enum.TryParse(enumFieldName, out result);
            return isSuccess;
        }

        private static string FindFieldName<T>(string description)
        {
            Argument.NotNull(description, nameof(description));

            var allFields = typeof(T).GetFields();
            foreach (var field in allFields)
            {
                var attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
                if (attr != null && ((DescriptionAttribute)attr).Description == description)
                {
                    return field?.Name;
                }
            }

            return null;
        }
    }
}
