using BetProto;
using Grpc.Net.Client;
using MassTransit;
using Oz.Bet.DataProvider;
using Oz.Bet.Events;
using Serilog;

namespace Oz.Bet.DataConsumer.Host;

internal class Worker : BackgroundService
{
    private readonly ILogger<Worker> logger;
    private readonly IPublishEndpoint publisher;
    private readonly IBulletinWriteRepository bulletinWriteRepository;

    public Worker(ILogger<Worker> logger, IPublishEndpoint publisher, IBulletinWriteRepository bulletinWriteRepository)
    {
        this.logger = logger;
        this.publisher = publisher;
        this.bulletinWriteRepository = bulletinWriteRepository;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            string address = Environment.GetEnvironmentVariable("ServerUrl");
            var channel = GrpcChannel.ForAddress(address);
            var client = new Bulletin.BulletinClient(channel);

            using var call = client.SubscribeBulletinStream(new Google.Protobuf.WellKnownTypes.Empty());
            while (call.ResponseStream.MoveNext(tokenSource.Token).Result)
            {
                try
                {
                    var bulletinEntity = Mapper.Map(call.ResponseStream.Current);
                    Log.Information($"CONSUMER Received bulletin {bulletinEntity.Version}");
                    await bulletinWriteRepository.ReplaceAsync(bulletinEntity);
                    await publisher.Publish(new EventChanged() { id = 1 });
                }
                catch (Exception e)
                {
                    Log.Error("Error Processing " + e.Message);
                    throw;
                }

            }
        }
    }
}