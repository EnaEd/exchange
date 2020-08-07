namespace Exchange.Mobile.Core.Models.RequestModels
{
    public class UploadOfferRequestModel
    {
        public string OfferPhoto { get; set; }
        public string OfferDescription { get; set; }
        public long CategoryId { get; set; }
        public User OfferOwner { get; set; }
    }
}
