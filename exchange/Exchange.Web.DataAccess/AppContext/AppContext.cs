using Exchange.Web.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Exchange.Web.DataAccess.AppContext
{
    public class AppContext : IdentityDbContext<User, IdentityRole<long>, long>
    {
        public AppContext()
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
