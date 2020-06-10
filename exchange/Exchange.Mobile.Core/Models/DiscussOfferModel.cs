using System;

namespace Exchange.Mobile.Core.Models
{
    public class DiscussOfferModel : BaseModel
    {
        public long OwnerId { get; set; }
        public string OwnerPhoneNumber { get; set; }
        public string PartnerPhoneNumber { get; set; }
        public string Conditions { get; set; }
        public long PartnerId { get; set; }
        public string OwnerPhotoOffer { get; set; }
        public string PartnerPhotoOffer { get; set; }
        public string Status { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
