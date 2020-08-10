using Exchange.Web.DataAccess.Entities.Base;

namespace Exchange.Web.DataAccess.Entities
{
    public class ChatEntity : BaseEntity
    {
        public string ChatName { get; set; }
        public long CreaterId { get; set; }
    }
}
