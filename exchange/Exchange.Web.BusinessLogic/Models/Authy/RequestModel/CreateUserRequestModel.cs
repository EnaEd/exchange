namespace Exchange.Web.BusinessLogic.Models.Authy.RequestModel
{
    public class CreateUserRequestModel
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CountryCode { get; set; }
    }
}
