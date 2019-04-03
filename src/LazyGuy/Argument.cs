using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LazyGuy.Constants;

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
    }
}
