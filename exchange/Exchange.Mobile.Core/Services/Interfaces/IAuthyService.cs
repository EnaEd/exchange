using Exchange.Mobile.Core.Models;
using Exchange.Mobile.Core.Models.ResponseModels;
using System.Threading.Tasks;

namespace Exchange.Mobile.Core.Services.Interfaces
{
    public interface IAuthyService
    {
        Task<AuthyResponseModel> AddUserAsync(User user);
        Task<AuthyOTPResponseModel> SendOTPAsync(long authyId);
        Task<AuthyVerifyResponse> VerifyTokenAsync(long token, long authyId);
    }
}
