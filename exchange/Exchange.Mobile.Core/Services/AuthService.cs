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
            PhoneRequestModel model = new PhoneRequestModel();
            model.PhoneNumber = phoneNumber;
            var response = await _apiService.ExecutePostAsync($"{ApplicationConfig.BaseUrl}api/account/checkuserexists", model);

            return Convert.ToBoolean(JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync()));
        }

        public async Task<bool> RegistrationAsync(User data)
        {
            var response = await _apiService.ExecutePostAsync($"{ ApplicationConfig.BaseUrl}api/account/registration", data);
            var test = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
            return test is User;

        }
    }
}
