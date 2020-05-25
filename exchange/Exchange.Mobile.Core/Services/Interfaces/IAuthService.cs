using System.Threading.Tasks;

namespace Exchange.Mobile.Core.Services.Interfaces
{
    public interface IAuthService<T> where T : class
    {
        Task<bool> CheckUserPhone(string phoneNumber);
        Task<bool> RegistrationAsync(T data);
        Task UpdatePushIdIfNeededAsync(string pushId);
    }
}
