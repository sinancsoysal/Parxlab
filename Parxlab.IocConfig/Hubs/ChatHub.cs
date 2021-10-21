using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Parxlab.Repository;

namespace Parxlab.IocConfig.Hubs
{
    public class ChatHub:Hub
    {
        private readonly IUnitOfWork unitOfWork;

        public ChatHub(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task SendMessage(string groupName, string msg, string type, string sender)
        {
            await Clients.OthersInGroup(groupName).SendAsync("MessageReceived", msg, type, sender);
        }
    }
}
