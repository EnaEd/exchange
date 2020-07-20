namespace Exchange.Mobile.Core.Models.ResponseModels
{
    public class AuthyVerifyResponse
    {
        public string Message { get; set; }
        public string Token { get; set; }
        public string Success { get; set; }
        public AuthyDevice Device { get; set; }
    }
}
