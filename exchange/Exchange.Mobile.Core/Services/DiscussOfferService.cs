using Exchange.Mobile.Core.Constants;
using Exchange.Mobile.Core.Models;
using Exchange.Mobile.Core.Models.RequestModels;
using Exchange.Mobile.Core.Services.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Exchange.Mobile.Core.Services
{
    public class DiscussOfferService : BaseApiService, IDiscussOfferService
    {
        private readonly HubConnection _hubConnection;
        private bool _isInitialized;


        public DiscussOfferService()
        {
            _hubConnection = new HubConnectionBuilder()
                  .WithUrl("http:barterok.somee.com/chat")
                  .WithAutomaticReconnect()
                  .Build();
        }

        public Action OnChatMessage { get; set; }

        public bool IsConnected => _hubConnection.State == HubConnectionState.Connected;


        public async Task<DiscussOfferModel> CreateDiscussOfferAsync(DiscussOfferModel model)
        {
            HttpResponseMessage response = await ExecutePostAsync($"{ApplicationConfig.BaseUrl}{Constant.Route.CREATE_DISCUSS_ROUTE}", model);
            DiscussOfferModel result = JsonConvert.DeserializeObject<DiscussOfferModel>(await response.Content.ReadAsStringAsync());
            return result;
        }

        public async Task<string> DeleteDiscussOfferAsync(DiscussOfferModel model)
        {
            HttpResponseMessage response = await ExecutePostAsync($"{ApplicationConfig.BaseUrl}{Constant.Route.DELETE_DISCUSS_ROUTE}", model);
            string result = JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
            return result;
        }

        public async Task Connect()
        {
            if (IsConnected)
            {
                await _hubConnection.StartAsync();
            }
            if (!_isInitialized)
            {
                _hubConnection.On("Resive", () => OnChatMessage?.Invoke());
            }
        }

        public async Task Disconnect()
        {
            if (IsConnected)
            {
                await _hubConnection.StopAsync();
            }
        }

        public async Task<IEnumerable<DiscussOfferModel>> GetUserDiscussAsync(DiscussOfferRequestModel model)
        {
            HttpResponseMessage response = await ExecutePostAsync($"{ApplicationConfig.BaseUrl}{Constant.Route.GET_USER_DISCUSS_ROUTE}", model);
            IEnumerable<DiscussOfferModel> result = JsonConvert.DeserializeObject<IEnumerable<DiscussOfferModel>>(await response.Content.ReadAsStringAsync());
            return result;
        }

        public async Task<DiscussOfferModel> UpdateDiscussOfferAsync(DiscussOfferModel model)
        {
            HttpResponseMessage response = await ExecutePostAsync($"{ApplicationConfig.BaseUrl}{Constant.Route.UPDATE_DISCUSS_ROUTE}", model);
            DiscussOfferModel result = JsonConvert.DeserializeObject<DiscussOfferModel>(await response.Content.ReadAsStringAsync());
            return result;
        }
    }
}
