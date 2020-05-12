using Exchange.Web.DataAccess.Entities.Base;

namespace Exchange.Web.DataAccess.Entities
{
    public class PhotoEntity : BaseEntity
    {
        public string PhotoSource { get; set; }
        public string Description { get; set; }
        public long CategoryId { get; set; }
        public long? UserId { get; set; }
        public UserEntity User { get; set; }

    }
}
