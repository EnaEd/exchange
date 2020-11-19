using Exchange.Web.BusinessLogic.Helpers;
using Exchange.Web.BusinessLogic.Models;
using Exchange.Web.BusinessLogic.Services.Interfaces;
using Exchange.Web.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exchange.Web.BusinessLogic.Services
{
    public class ChatHubService : Hub, IChatHubService
    {
        private IHubContext<ChatHubService> _context;
        private readonly static ConnectionMappingHelper<string> _connections =
           new ConnectionMappingHelper<string>();
        private readonly UserManager<UserEntity> _userManager;

        public ChatHubService(UserManager<UserEntity> userManager, IHubContext<ChatHubService> context)
        {
            _userManager = userManager;
            _context = context;
        }

        public override Task OnConnectedAsync()
        {
            var test = Context.GetHttpContext();
            var test1 = test.Request.Cookies;
            string name = Context.User.Identity.Name;
            _connections.Add(name, Context.ConnectionId);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            string name = Context.User.Identity.Name;

            _connections.Remove(name, Context.ConnectionId);

            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendAll(string userName, string message)
        {
            await Clients.All.SendAsync("Receive", userName, message);
        }
        public async Task SendOneToOne(ChatRequestModel model)
        {
            var caller = await _userManager.FindByIdAsync(model.PrticipantIds.First().ToString());
            var opponent = await _userManager.FindByIdAsync(model.PrticipantIds.Last().ToString());
            //need map if user has more one connection
            List<string> connectionIds = new List<string>();
            connectionIds = _connections.GetConnections(caller.Email).ToList();
            connectionIds.Add(_connections.GetConnections(opponent.Email).FirstOrDefault());
            await _context.Clients.Clients(connectionIds).SendAsync("Receive");
        }

    }
}
