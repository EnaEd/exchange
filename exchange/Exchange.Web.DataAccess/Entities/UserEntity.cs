using Exchange.Web.DataAccess.Entities.Base;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Exchange.Web.DataAccess.Entities
{
    public class UserEntity : IdentityUser<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public LocationModelEntity Location { get; set; }
        public ICollection<PhotoEntity> Photos { get; set; } = new List<PhotoEntity>();

    }
}
