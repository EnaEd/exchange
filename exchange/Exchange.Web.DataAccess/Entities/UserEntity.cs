using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Exchange.Web.DataAccess.Entities
{
    public class UserEntity : IdentityUser<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string OneSignalId { get; set; }
        public ICollection<PhotoEntity> Photos { get; set; } = new List<PhotoEntity>();

    }
}
