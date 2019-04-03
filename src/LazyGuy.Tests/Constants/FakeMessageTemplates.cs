namespace LazyGuy.Tests.Constants
{
    public class FakeMessageTemplates
    {
        public static string ArgumentNull
        {
            get
            {
                string wildcard = $"Argument '*' should not be null.";
                return $"*{wildcard}*";
            }
        }

        public static string ArgumentEmpty
        {
            get
            {
                string wildcard = $"Argument '*' should not be empty.";
                return $"*{wildcard}*";
            }
        }

        public static string ArgumentOutOfRange
        {
            get
            {
                string wildcard = $"Argument '*' out of range.";
                return $"*{wildcard}*";
            }
        }

        public static string InvalidArrayTypeForParams
        {
            get
            {
                string wildcard = $"Arugment '*' exist invalid type(s)";
                return $"*{wildcard}*";
            }
        }

        public static string ValueNotInEnum
        {
            get
            {
                string wildcard = $"Value of argument '*' not defind in enum.";
                return $"*{wildcard}*";
            }
        }
    }
}
