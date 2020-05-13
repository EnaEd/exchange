using Exchange.Web.BusinessLogic.Models;
using System.Threading.Tasks;

namespace Exchange.Web.BusinessLogic.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<UserModel> Registration(UserModel model);
    }
}
