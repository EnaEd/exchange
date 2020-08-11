using Exchange.Web.BusinessLogic.Models.Base;
using System.Collections.Generic;

namespace Exchange.Web.BusinessLogic.Models
{
    public class ChatRequestModel : BaseModel
    {
        public string ChatName { get; set; }
        public long CreaterId { get; set; }
        public List<long> PrticipantIds { get; set; }
    }
}
