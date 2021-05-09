using System;
using System.ComponentModel;

namespace LazyGuy.Utils
{
    // TODO: migrate to extension methods.
    public static class EnumUtils
    {
        public static T ParseDescription<T>(string description)
            where T : Enum
        {
            Argument.NotNull(description, nameof(description));

            string enumFieldName = FindFieldName<T>(description);
            Argument.NotNull(enumFieldName, nameof(enumFieldName));

            T result = (T)Enum.Parse(typeof(T), enumFieldName);
            return result;
        }

        // Add additional restrict 'struct' for T so that can use Enum.TryParse()
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
