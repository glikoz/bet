using MassTransit;
using Oz.Bet;
using Oz.Bet.Data;
using Oz.Bet.Notification.App;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder.WithOrigins("http://localhost")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
    );
});
builder.Services.AddSignalR().AddHubOptions<SampleHub>(options => options.EnableDetailedErrors = true);
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<EventChangedConsumer>();
    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(Environment.GetEnvironmentVariable("RabbitUrl"));
        cfg.ReceiveEndpoint("order-service", e =>
        {
            e.ConfigureConsumer<EventChangedConsumer>(ctx);
        });
    });
});
builder.Services.AddScoped<EventChangedConsumer>();
builder.Services.AddScoped<IBulletinReadRepository,RedisBulletinReadRepository>();
builder.Services.AddStackExchangeRedisCache(config =>
{
    config.Configuration = Environment.GetEnvironmentVariable("Redis");
});

LogHelper.Init(builder.Services);
var app = builder.Build();
app.UseCors("CorsPolicy");
app.MapHub<SampleHub>("/hubs/oddchange");
await app.RunAsync();