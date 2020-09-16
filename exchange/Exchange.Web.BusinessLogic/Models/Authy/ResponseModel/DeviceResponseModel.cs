namespace Exchange.Web.BusinessLogic.Models.Authy.ResponseModel
{
    public class DeviceResponseModel
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string IP { get; set; }
        public string Region { get; set; }
        public string RegistrationCity { get; set; }
        public string RegistrationCountry { get; set; }
        public long RegistrationDeviceId { get; set; }
        public string RegistrationIP { get; set; }
        public string RegistrationMethod { get; set; }
        public string RegistrationRegion { get; set; }
        public string OSType { get; set; }
        public long LastAccountRecoveryAt { get; set; }
        public long Id { get; set; }
        public long RegistrationDate { get; set; }
    }
}
