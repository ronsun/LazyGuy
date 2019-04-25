using System;
using System.ComponentModel;

namespace LazyGuy.Utils
{
    public class EnumUtils
    {
        public static T ParseDesciption<T>(string description)
            where T : Enum
        {
            Argument.NotNullOrEmpty(description, nameof(description));

            // Do not use linq here for readibility
            string enumName = null;
            var allFields = typeof(T).GetFields();
            foreach (var field in allFields)
            {
                var attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
                if (attr != null && ((DescriptionAttribute)attr).Description == description)
                {
                    enumName = field.Name;
                    break;
                }
            }

            Argument.InRange(() => enumName != null, nameof(description));

            T result = (T)Enum.Parse(typeof(T), enumName);
            return result;
        }

        //TODO: TryParseDesciption()
    }
}
