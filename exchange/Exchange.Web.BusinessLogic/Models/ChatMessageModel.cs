using Exchange.Web.BusinessLogic.Models.Base;
using System;

namespace Exchange.Web.BusinessLogic.Models
{
    public class ChatMessageModel : BaseModel
    {
        public long ChatId { get; set; }
        public long UserId { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
