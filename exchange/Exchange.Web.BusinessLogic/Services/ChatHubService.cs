using Exchange.Web.BusinessLogic.Helpers;
using Exchange.Web.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exchange.Web.BusinessLogic.Services
{
    public class ChatHubService : Hub, IChatHubService
    {
        private readonly static ConnectionMappingHelper<string> _connections =
           new ConnectionMappingHelper<string>();


        public override Task OnConnectedAsync()
        {
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
        public async Task SendOneToOne(string callerIdentityEmail, string opponentIdentityEmail, string message)
        {
            //need map if user has more one connection
            List<string> connectionIds = new List<string>();
            connectionIds = _connections.GetConnections(callerIdentityEmail).ToList();
            connectionIds.AddRange(_connections.GetConnections(opponentIdentityEmail));

            await Clients.Clients(connectionIds).SendAsync("Receive");
        }

    }
}
