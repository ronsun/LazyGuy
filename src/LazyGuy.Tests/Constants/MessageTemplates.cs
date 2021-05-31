namespace LazyGuy.Tests.Constants
{
    public class MessageTemplates
    {
        public static string InvalidArrayTypeForParams
        {
            get
            {
                string wildcard = $"Argument '*' exist invalid type(s)";
                return $"*{wildcard}*";
            }
        }
    }
}
