using Exchange.Web.DataAccess.Entities.Base;

namespace Exchange.Web.DataAccess.Entities
{
    public class ChatPartyEntity : BaseEntity
    {
        public long ChatId { get; set; }
        public long UserId { get; set; }
    }
}
