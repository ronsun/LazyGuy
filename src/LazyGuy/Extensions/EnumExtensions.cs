using System;
using System.Linq;
using LazyGuy.Constants;

namespace LazyGuy.Extensions
{
    public static class EnumExtensions
    {
        public static bool In(this Enum target, params Enum[] list)
        {
            Argument.ArrayTypeMatched(list, nameof(list), target.GetType());
            Argument.EnumDefined(target, nameof(target));
            foreach (var item in list)
            {
                Argument.EnumDefined(item, nameof(list));
            }

            return list.Contains(target);
        }
    }
}
