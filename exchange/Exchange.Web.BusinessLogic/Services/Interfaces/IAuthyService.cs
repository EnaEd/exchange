using Exchange.Web.BusinessLogic.Models.Authy;
using Exchange.Web.BusinessLogic.Models.Authy.RequestModel;
using Exchange.Web.BusinessLogic.Models.Authy.ResponseModel;
using System.Threading.Tasks;

namespace Exchange.Web.BusinessLogic.Services.Interfaces
{
    public interface IAuthyService
    {
        public Task<AuthyOTPCodeResponse> CreateAuthyUserAsync(CreateUserRequestModel model);
        public Task<AuthyBaseModel> SendOTPCodeAsync(int authyId);
        public Task<AuthyVerifyCodeResponseModel> VerifyOTPCodeAsync(int authyId, string token);
    }
}
