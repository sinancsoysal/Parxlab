using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Parxlab.IocConfig.Hubs
{
    public class SensorHub : Hub
    {
        public async Task UpdateStatus(string groupName, string sensorId,int status)
        {
            await Clients.OthersInGroup(groupName).SendAsync("UpdateStatus", sensorId, status);
        }
    }
}
