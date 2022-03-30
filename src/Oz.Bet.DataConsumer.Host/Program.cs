using MassTransit;
using Oz.Bet;
using Oz.Bet.DataConsumer.Host;
using Oz.Bet.DataProvider;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureAppConfiguration((ctx, config) =>
{
    config.AddEnvironmentVariables();

});

builder.ConfigureServices((context, services) =>
{
    services.AddHostedService<Worker>();
    //services.AddStackExchangeRedisExtensions<SystemTextJsonSerializer>((options) =>
    //{
    //    return new[]{new RedisConfiguration
    //    {
    //        ConnectionString= "redis:6379,abortConnect=false,SyncTimeout=10000,ConnectTimeout=2000"
    //    } };
    //});
    services.AddStackExchangeRedisCache(config => config.Configuration = Environment.GetEnvironmentVariable("Redis"));
    services.AddSingleton<IBulletinWriteRepository, RedisBulletinWriteRepository>();

    services.AddMassTransit(x =>
    {
        x.UsingRabbitMq((ctx, cfg) =>
        {
            cfg.Host(Environment.GetEnvironmentVariable("RabbitUrl"));
            cfg.ConfigureEndpoints(ctx);
        });
    });
    services.AddOptions<MassTransitHostOptions>()
        .Configure(options =>
        {
            options.WaitUntilStarted = true;
        });
    LogHelper.Init(services);
});


await builder.Build().RunAsync();
