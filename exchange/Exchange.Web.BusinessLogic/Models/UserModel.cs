using Exchange.Web.BusinessLogic.Models.Base;
using System.Collections.Generic;

namespace Exchange.Web.BusinessLogic.Models
{
    public class UserModel : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string OneSignalId { get; set; }
        public ICollection<PhotoModel> Photos { get; set; } = new List<PhotoModel>();
    }
}
