using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;

namespace Oz.Bet.Notification.App;

[EnableCors("CorsPolicy")]
public class SampleHub : Hub<ISampleHub>
{
    public async Task SendMessage(BulletinEntity bulletin)
    {
        await Clients.All.ChangeOccured(bulletin);
    }
}