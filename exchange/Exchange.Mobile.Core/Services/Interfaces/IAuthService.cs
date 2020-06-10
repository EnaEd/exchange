using Exchange.Mobile.Core.Models;
using Exchange.Mobile.Core.Models.RequestModels;
using System.Threading.Tasks;

namespace Exchange.Mobile.Core.Services.Interfaces
{
    public interface IAuthService<T> where T : class
    {
        Task<bool> CheckUserPhone(string phoneNumber);
        Task<bool> RegistrationAsync(T data);
        Task<T> UpdatePushIdIfNeededAsync(string phoneNumber, string pushId);
        Task<T> GetUserByIdAsync(long id);
        Task<User> GetUserByPhone(PhoneRequestModel model);
    }
}
