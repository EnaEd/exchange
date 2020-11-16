namespace Exchange.Web.BusinessLogic.Models.Authy.RequestModel
{
    public class VerifyCodeRequestModel
    {
        public int AuthyId { get; set; }
        public string Token { get; set; }
        public string Phone { get; set; }
        public string CountryCode { get; set; }
    }
}
