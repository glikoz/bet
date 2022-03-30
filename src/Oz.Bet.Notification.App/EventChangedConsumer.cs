using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Oz.Bet.Data;
using Oz.Bet.Events;
using Serilog;

namespace Oz.Bet.Notification.App;

public class EventChangedConsumer : IConsumer<EventChanged>
{
    private readonly IHubContext<SampleHub, ISampleHub> _hub;
    private readonly IBulletinReadRepository bulletinReadRepository;

    public EventChangedConsumer(IHubContext<SampleHub, ISampleHub> hub, IBulletinReadRepository bulletinReadRepository)
    {
        _hub = hub;
        this.bulletinReadRepository = bulletinReadRepository;
    }
    public async Task Consume(ConsumeContext<EventChanged> context)
    {
        Log.Information("NOTIF EventChangedConsumer: {@Message}", context.Message);
        var bulletin = await bulletinReadRepository.GetBulletinAsync();
        await _hub.Clients.All.ChangeOccured(bulletin);
    }
}