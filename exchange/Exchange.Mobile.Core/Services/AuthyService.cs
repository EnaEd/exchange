using Exchange.Mobile.Core.Constants;
using Exchange.Mobile.Core.Models;
using Exchange.Mobile.Core.Models.ResponseModels;
using Exchange.Mobile.Core.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Exchange.Mobile.Core.Services
{
    public class AuthyService : IAuthyService
    {
        public static readonly HttpClient httpClient = CreateHttpClient();
        private static HttpClient CreateHttpClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(Constant.AuthyConstant.AUTHY_BASE_URL);
            client.DefaultRequestHeaders.Add(Constant.AuthyConstant.DEFAULT_GUARD_HEADER, Constant.AuthyConstant.AUTHY_API_KEY);
            return client;
        }

        public async Task<AuthyResponseModel> AddUserAsync(User user)
        {
            var requestContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("user[email]",user.Email),
                new KeyValuePair<string, string>("user[cellphone]",user.Phone),
                new KeyValuePair<string, string>("user[country_code]",user.CountryCode)
            });

            HttpResponseMessage response = await httpClient.PostAsync(Constant.AuthyConstant.ADD_USER_URL, requestContent);
            if (!response.IsSuccessStatusCode)
            {
                var responseModel = new AuthyResponseModel
                {
                    Code = $"{response.StatusCode} {response.ReasonPhrase}",
                    Success = false,
                    Message = Constant.Shared.FAIL_CREATE_AUTHY_USER
                };
                return responseModel;
            }
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AuthyResponseModel>(content);
        }

        public async Task<AuthyOTPResponseModel> SendOTPAsync(long authyId)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"{Constant.AuthyConstant.SEND_OTP_URL}/{authyId}");
            if (!response.IsSuccessStatusCode)
            {
                var responseModel = new AuthyOTPResponseModel
                {
                    Success = false,
                    Message = $"{response.StatusCode} {response.ReasonPhrase}"
                };
                return responseModel;
            }
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AuthyOTPResponseModel>(content);
        }

        public async Task<AuthyVerifyResponse> VerifyTokenAsync(long token, long authyId)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"{Constant.AuthyConstant.VERIFY_TOKEN_URL}/{token}/{authyId}");
            if (!response.IsSuccessStatusCode)
            {
                return default;
            }
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AuthyVerifyResponse>(content);
        }
    }
}
