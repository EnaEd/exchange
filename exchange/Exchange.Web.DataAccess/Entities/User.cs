using Microsoft.AspNetCore.Identity;

namespace Exchange.Web.DataAccess.Entities
{
    public class User : IdentityUser<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string PhotoForExchange { get; set; }

    }
}
