using LazyGuy.Constants;
using System;
using System.Linq;

namespace LazyGuy.Extensions
{
    public static class EnumExtensions
    {
        public static bool In(this Enum target, params Enum[] list)
        {
            bool anyInvalidType = list.Any(r => r.GetType() != target.GetType());
            if (anyInvalidType)
            {
                string msg = string.Format(MessageTemplates.InvalidArrayTypeForParams, nameof(list));
                throw new ArrayTypeMismatchException(msg);
            }

            var isTargetOutOfRange = !Enum.IsDefined(target.GetType(), target);
            if (isTargetOutOfRange)
            {
                string msg = string.Format(MessageTemplates.ValueNotInEnum, nameof(target));
                throw new ArgumentOutOfRangeException(msg);
            }

            var anyItemInListOutOfRange = list.Any(r => !Enum.IsDefined(r.GetType(), r));
            if (anyItemInListOutOfRange)
            {
                string msg = string.Format(MessageTemplates.ValueNotInEnum, nameof(list));
                throw new ArgumentOutOfRangeException(msg);
            }

            return list.Contains(target);
        }
    }
}
