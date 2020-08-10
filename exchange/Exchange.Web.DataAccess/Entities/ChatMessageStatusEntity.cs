using Exchange.Web.DataAccess.Entities.Base;

namespace Exchange.Web.DataAccess.Entities
{
    public class ChatMessageStatusEntity : BaseEntity
    {
        public long MessageId { get; set; }
        public long UserId { get; set; }
        public bool IsRead { get; set; }
    }
}
