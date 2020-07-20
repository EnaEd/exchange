namespace Exchange.Mobile.Core.Models.ResponseModels
{
    public class AuthyResponseModel : BaseModel
    {
        public string Message { get; set; }
        public AuthyUser User { get; set; }
        public bool Success { get; set; }
    }
}
