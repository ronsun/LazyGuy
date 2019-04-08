using System;
using System.Linq;

namespace LazyGuy
{
    public static class Argument
    {
        public static void NotNull<T>(T arg, string argName)
        {
            if (arg == null)
            {
                throw new ArgumentNullException($"Argument '{argName}' should not be null.");
            }
        }

        public static void NotNullOrEmpty<T>(T arg, string argName)
        {
            NotNull(arg, argName);

            var exception = new ArgumentOutOfRangeException($"Argument '{argName}' should not be empty.");

            if (arg is string)
            {
                var stringArg = arg as string;
                if (stringArg == string.Empty)
                {
                    throw exception;
                }
            }

            if (arg is Array)
            {
                var arrayArg = arg as Array;
                if (arrayArg.Length == 0)
                {
                    throw exception;
                }
            }
        }

        public static void EnumDefined(Enum arg, string argName)
        {
            string errorMessage = $"Value of argument '{argName}' not defind in enum.";
            InRange(() => Enum.IsDefined(arg.GetType(), arg), argName, errorMessage);
        }

        public static void InRange(Func<bool> rule, string argName, string errorMessage = null)
        {
            bool matchRule = rule();
            if (!matchRule)
            {
                errorMessage = errorMessage ?? $"Argument '{argName}' out of range.";
                throw new ArgumentOutOfRangeException(errorMessage, new Exception());
            }
        }

        public static void ArrayTypeMatched<T>(T[] arg, string argName, Type expectedType, string errorMessage = null)
        {
            bool matchRule = arg.All(r => r.GetType() == expectedType);
            if (!matchRule)
            {
                errorMessage = errorMessage ?? $"Arugment '{argName}' exist invalid type(s)";
                throw new ArrayTypeMismatchException(errorMessage);
            }
        }
    }
}
