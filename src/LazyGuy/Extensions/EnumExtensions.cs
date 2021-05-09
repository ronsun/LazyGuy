using System;
using System.ComponentModel;
using System.Linq;

namespace LazyGuy.Extensions
{
    /// <summary>
    /// Extension methods for Enum.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Check is specific enum '<paramref name="target"/>' in
        /// a list of enum values '<paramref name="list"/>'.
        /// </summary>
        /// <typeparam name="T">Type of enum.</typeparam>
        /// <param name="target">The enum to be checked.</param>
        /// <param name="list">The list of enums.</param>
        /// <returns>Whether <paramref name="target"/> in <paramref name="list"/>.</returns>
        /// <exception cref="ArrayTypeMismatchException">
        ///     Multiple enum types in parameter <paramref name="list"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Any value of argument not defined in its enum type.
        /// </exception>
        public static bool In<T>(this T target, params T[] list)
            where T : Enum
        {
            Argument.EnumDefined(target, nameof(target));
            foreach (var item in list)
            {
                Argument.EnumDefined(item, nameof(list));
            }

            return list.Contains(target);
        }

        /// <summary>
        /// Get Description of DescriptionAttribute specific enum.
        /// </summary>
        /// <typeparam name="T">Type of enum.</typeparam>
        /// <param name="target">The target enum.</param>
        /// <returns>Description.</returns>
        public static string GetDescription<T>(this T target)
            where T : Enum
        {
            string enumFieldName = target.ToString();
            var element = typeof(T).GetField(enumFieldName);
            var attribute = Attribute.GetCustomAttribute(element, typeof(DescriptionAttribute));
            if (attribute != null)
            {
                return ((DescriptionAttribute)attribute).Description;
            }

            return enumFieldName;
        }
    }
}
