using Exchange.Web.DataAccess.Entities;

namespace Exchange.Web.BusinessLogic.Models
{
    public class OfferRequestModel
    {
        public string OfferPhoto { get; set; }
        public string OfferDescription { get; set; }
        public long CategoryId { get; set; }
        public OfferOwnerDetail OfferOwner { get; set; }
        public long UserId { get; set; }
        public UserEntity User { get; set; }
    }
}
