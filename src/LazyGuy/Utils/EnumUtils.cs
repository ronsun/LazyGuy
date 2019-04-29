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
            Argument.NotNullOrEmpty(description, nameof(description));

            FieldInfo enumField = ParseDescription(typeof(T), description);
            Argument.InRange(() => enumField != null, nameof(description));

            T result = (T)Enum.Parse(typeof(T), enumField.Name);
            return result;
        }

        //TODO: TryParseDesciption()

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
