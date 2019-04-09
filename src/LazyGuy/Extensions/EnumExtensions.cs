using System;
using System.ComponentModel;
using System.Linq;

namespace LazyGuy.Extensions
{
    public static class EnumExtensions
    {
        public static bool In<T>(this T target, params T[] list)
            where T : Enum
        {
            Argument.ArrayTypeMatched(list, nameof(list), target.GetType());
            Argument.EnumDefined(target, nameof(target));
            foreach (var item in list)
            {
                Argument.EnumDefined(item, nameof(list));
            }

            return list.Contains(target);
        }

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
