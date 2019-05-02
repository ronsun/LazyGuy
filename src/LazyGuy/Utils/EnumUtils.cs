using System;
using System.ComponentModel;
using System.Reflection;

namespace LazyGuy.Utils
{
    public class EnumUtils
    {
        public static T ParseDescription<T>(string description)
            where T : Enum
        {
            Argument.NotNull(description, nameof(description));

            FieldInfo enumField = ParseDescription(typeof(T), description);
            Argument.InRange(() => enumField != null, nameof(description));

            T result = (T)Enum.Parse(typeof(T), enumField.Name);
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

            FieldInfo enumField = ParseDescription(typeof(T), description);
            if (enumField == null)
            {
                return false;
            }

            bool isSuccess = Enum.TryParse(enumField.Name, out result);
            return isSuccess;
        }

        private static FieldInfo ParseDescription(Type enumType, string description)
        {
            var allFields = enumType.GetFields();
            foreach (var field in allFields)
            {
                var attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
                if (attr != null && ((DescriptionAttribute)attr).Description == description)
                {
                    return field;
                }
            }

            return null;
        }
    }
}
