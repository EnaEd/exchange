using Exchange.Web.BusinessLogic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exchange.Web.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        public Task<IEnumerable<UserModel>> GetAllAsync();
    }
}
