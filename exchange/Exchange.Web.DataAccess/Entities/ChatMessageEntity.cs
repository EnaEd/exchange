using Exchange.Web.DataAccess.Entities.Base;
using System;

namespace Exchange.Web.DataAccess.Entities
{
    public class ChatMessageEntity : BaseEntity
    {
        public long ChatId { get; set; }
        public long UserId { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
