using System;

namespace LazyGuy
{
    /// <summary>
    /// Validations for argument.
    /// </summary>
    public static class Argument
    {
        /// <summary>
        /// Throw exception if argement is null.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="arg"/>.</typeparam>
        /// <param name="arg">Argument.</param>
        /// <param name="argName">Argument name.</param>
        public static void NotNull<T>(T arg, string argName)
        {
            if (arg == null)
            {
                throw new ArgumentNullException(argName);
            }
        }

        /// <summary>
        /// Throw exception if argement is null or empty.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="arg"/>.</typeparam>
        /// <param name="arg">Argument.</param>
        /// <param name="argName">Argument name.</param>
        public static void NotNullOrEmpty<T>(T arg, string argName)
        {
            var exception = new ArgumentOutOfRangeException(argName);

            if (arg == null)
            {
                throw exception;
            }

            var stringArg = arg as string;
            if (stringArg == string.Empty)
            {
                throw exception;
            }

            var arrayArg = arg as Array;
            if (arrayArg?.Length == 0)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Throw exception if argement not defined in current enum type.
        /// </summary>
        /// <param name="arg">Argument.</param>
        /// <param name="argName">Argument name.</param>
        public static void EnumDefined(Enum arg, string argName)
        {
            if (Enum.IsDefined(arg.GetType(), arg) == false)
            {
                throw new ArgumentOutOfRangeException(argName);
            }
        }

        /// <summary>
        /// Throw exception if argement not in range.
        /// </summary>
        /// <param name="rule">Rule.</param>
        /// <param name="argName">Argument name.</param>
        public static void InRange(Func<bool> rule, string argName)
        {
            bool matchRule = rule();
            if (!matchRule)
            {
                throw new ArgumentOutOfRangeException(argName);
            }
        }
    }
}
