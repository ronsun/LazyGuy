using LazyGuy.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyGuy.Extensions
{
    public static class EnumExtensions
    {
        public static bool In(this Enum target, params Enum[] list)
        {
            bool anyInvalidType = list.Any(r => r.GetType() != target.GetType());
            if (anyInvalidType)
            {
                throw new ArrayTypeMismatchException(ExceptionMessages.ArgumentNullMessage);
            }

            var isTargetOutOfRange = !Enum.IsDefined(target.GetType(), target);
            var anyItemInListOutOfRange = list.Any(r => !Enum.IsDefined(r.GetType(), r));
            if (isTargetOutOfRange || anyItemInListOutOfRange)
            {
                throw new ArgumentOutOfRangeException(ExceptionMessages.ValueNotDefinedInEnum);
            }

            return list.Contains(target);
        }
    }
}
