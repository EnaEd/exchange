namespace Exchange.Web.BusinessLogic.Models
{
    public class FilterRequestModel
    {
        public long? UserId { get; set; }
        public int? CategoryId { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public float? Long { get; set; }
        public float? Lat { get; set; }
        public long? LastOfferId { get; set; }
    }
}
