using Exchange.Web.BusinessLogic.Models;
using Exchange.Web.BusinessLogic.Models.Authy;
using Exchange.Web.BusinessLogic.Models.Authy.RequestModel;
using Exchange.Web.BusinessLogic.Models.Authy.ResponseModel;
using System.Threading.Tasks;

namespace Exchange.Web.BusinessLogic.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<UserModel> RegistrationAsync(UserModel model);
        public Task<UserModel> IsUserExistAsync(string phoneNumber);
        public Task<UserModel> UpdateUserIfNeeded(UserModel model);
        public Task<AuthyBaseModel> SignInUser(PhoneRequestModel model);
        public Task<AuthyVerifyCodeResponseModel> VerifyOtpCodeAsync(VerifyCodeRequestModel model);
    }
}
