namespace Exchange.Mobile.Core.Constants
{
    public static partial class Constant
    {

        public static class AuthyConstant
        {
            public const string AUTHY_API_KEY = "nhMV4vVdkT8NHP6yu8g6D4JfF8OJw678";
            public const string AUTHY_BASE_URL = "https://api.authy.com/";

            public const string DEFAULT_GUARD_HEADER = "X-Authy-API-Key";

            public const string ADD_USER_URL = "protected/json/users/new";
            public const string SEND_OTP_URL = "protected/json/sms";
            public const string VERIFY_TOKEN_URL = "protected/json/verify";
        }
    }
}
