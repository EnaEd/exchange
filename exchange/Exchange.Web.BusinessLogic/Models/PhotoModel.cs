namespace Exchange.Web.BusinessLogic.Models
{
    public class PhotoModel
    {
        public string PhotoSource { get; set; }
        public string Description { get; set; }
        public long CategoryId { get; set; }
        public long? UserId { get; set; }
        public UserModel User { get; set; }
    }
}
