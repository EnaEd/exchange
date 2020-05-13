using Exchange.Web.DataAccess.Entities;
using Exchange.Web.Shared.Enums;

namespace Exchange.Web.BusinessLogic.Models
{
    public class OfferRequestModel
    {
        public string OfferPhoto { get; set; }
        public string OfferDescription { get; set; }
        public Enum.ExchangeCategory Category { get; set; }
        public OfferOwnerDetail OfferOwner { get; set; }
        public long UserId { get; set; }
        public UserEntity User { get; set; }
    }
}
