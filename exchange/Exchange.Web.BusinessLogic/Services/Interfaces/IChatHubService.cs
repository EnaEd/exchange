using System.Threading.Tasks;

namespace Exchange.Web.BusinessLogic.Services.Interfaces
{
    public interface IChatHubService
    {
        public Task SendAll(string userName, string message);
        public Task SendOneToOne(string callerIdentityName, string opponentIdentityName, string message);
    }
}
