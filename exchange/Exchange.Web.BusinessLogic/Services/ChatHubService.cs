using Exchange.Web.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Exchange.Web.BusinessLogic.Services
{
    public class ChatHubService : Hub, IChatHubService
    {
        public async Task SendAll(string userName, string message)
        {
            await Clients.All.SendAsync("Receive", userName, message);
        }
        public async Task SendOneToOne(string callerIdentityName, string opponentIdentityName, string message)
        {
            await Clients.Users(new string[] { callerIdentityName, opponentIdentityName }).SendAsync("Receive");
        }

    }
}
