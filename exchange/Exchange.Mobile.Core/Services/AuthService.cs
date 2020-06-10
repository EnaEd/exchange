using Exchange.Mobile.Core.Constants;
using Exchange.Mobile.Core.Models;
using Exchange.Mobile.Core.Models.RequestModels;
using Exchange.Mobile.Core.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Exchange.Mobile.Core.Services
{
    public class AuthService : BaseApiService, IAuthService<User>
    {
        private readonly BaseApiService _apiService;

        public AuthService()
        {
            _apiService = new BaseApiService();
        }

        public async Task<bool> CheckUserPhone(string phoneNumber)
        {
            PhoneRequestModel model = new PhoneRequestModel
            {
                PhoneNumber = phoneNumber
            };
            var response = await _apiService.ExecutePostAsync($"{ApplicationConfig.BaseUrl}api/account/checkuserexists", model);

            return Convert.ToBoolean(JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync()));
        }

        public async Task<User> GetUserByIdAsync(long id)
        {

            var response = await _apiService.ExecuteGetAsync<User>($"{ApplicationConfig.BaseUrl}api/user/{id}");
            var user = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
            return user;
        }

        public async Task<User> GetUserByPhone(PhoneRequestModel model)
        {
            var response = await _apiService.ExecutePostAsync($"{ApplicationConfig.BaseUrl}{Constant.Route.GET_USER_BY_PHONE_ROUTE}", model);
            var result = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
            return result as User;
        }

        public async Task<bool> RegistrationAsync(User data)
        {
            var response = await _apiService.ExecutePostAsync($"{ ApplicationConfig.BaseUrl}api/account/registration", data);
            var test = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
            return test is User;

        }

        public async Task<User> UpdatePushIdIfNeededAsync(string phoneNumber, string pushId)
        {

            PhoneRequestModel model = new PhoneRequestModel
            {
                PhoneNumber = phoneNumber
            };

            var response = await _apiService.ExecutePostAsync($"{ApplicationConfig.BaseUrl}api/user/getuser", model);
            var user = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
            if (user.OneSignalId == pushId)
            {
                return user;
            }
            user.OneSignalId = pushId;
            response = await _apiService.ExecutePostAsync($"{ApplicationConfig.BaseUrl}api/account/updateuser", user);

            return JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
        }
    }
}
