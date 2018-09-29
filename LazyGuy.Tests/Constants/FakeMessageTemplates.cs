using LazyGuy.Constants;

namespace LazyGuy.Tests.Constants
{
    public class FakeMessageTemplates
    {
        public static string ArgumentNull
        {
            get
            {
                string wildcard = string.Format(MessageTemplates.ArgumentNull, "*");
                return $"*{wildcard}*";
            }
        }

        public static string ArgumentEmpty
        {
            get
            {
                string wildcard = string.Format(MessageTemplates.ArgumentEmpty, "*");
                return $"*{wildcard}*";
            }
        }

        public static string NumberMustGreatherThanAnother
        {
            get
            {
                string wildcard = string.Format(MessageTemplates.NumberMustGreatherThanAnother, "*", "*");
                return $"*{wildcard}*";
            }
        }

        public static string InvalidArrayTypeForParams
        {
            get
            {
                string wildcard = string.Format(MessageTemplates.InvalidArrayTypeForParams, "*");
                return $"*{wildcard}*";
            }
        }

        public static string ValueNotInEnum
        {
            get
            {
                string wildcard = string.Format(MessageTemplates.ValueNotInEnum, "*");
                return $"*{wildcard}*";
            }
        }
    }
}
