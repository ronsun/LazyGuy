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
    }
}
