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
                throw new ArgumentNullException($"Argument '{argName}' should not be null.");
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
            var exception = new ArgumentOutOfRangeException($"Argument '{argName}' should not be null or empty.");

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
            string errorMessage = $"Value of argument '{argName}' not defind in enum.";
            InRange(() => Enum.IsDefined(arg.GetType(), arg), argName, errorMessage);
        }

        /// <summary>
        /// Throw exception if argement not in range.
        /// </summary>
        /// <param name="rule">Rule.</param>
        /// <param name="argName">Argument name.</param>
        /// <param name="errorMessage">Error message.</param>
        public static void InRange(Func<bool> rule, string argName, string errorMessage = null)
        {
            bool matchRule = rule();
            if (!matchRule)
            {
                errorMessage = errorMessage ?? $"Argument '{argName}' out of range.";
                throw new ArgumentOutOfRangeException(errorMessage, new Exception());
            }
        }
    }
}
