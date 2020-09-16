namespace Exchange.Web.Shared.Constants
{
    public partial class Constant
    {

        public static class ErrorInfo
        {
            public const string REGISTRATION_FAIL = "registration fail";

            public const string USER_NOT_FOUND = "user not found";
            public const string NO_DATA_FOR_UPLOAD = "offer photo or photo description is empty";

            public const string DISCUSS_NOT_FOUND = "discuss not found";

            public const string FAIL_CREATE_CHAT = "fail occurs creating chat";
            public const string FAIL_CREATE_MESSAGE = "fail occurs creating message";
            public const string FAIL_CREATE_CHATPARTY = "fail occurs creating chat party";

            public const string AUTHY_FAIL_CREATE_USER = "somethig went wrong. User create fail";
            public const string AUTHY_FAIL_SEND_OTP = "somethig went wrong. Send otp fail";
            public const string AUTHY_FAIL_VERIFY_OTP_CODE = "somethig went wrong. Verify otp code fail";

        }
    }
}
