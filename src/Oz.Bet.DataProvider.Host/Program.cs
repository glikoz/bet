using Microsoft.AspNetCore.Server.Kestrel.Core;
using Oz.Bet;
using Oz.Bet.DataProvider;
using Oz.Bet.DataProvider.Host.Services;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<DataGenerator>();

// Add services to the container.
builder.Services.AddGrpc();
builder.WebHost.UseKestrel(so =>
{
    so.Listen(IPAddress.Any, 5001, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});
LogHelper.Init(builder.Services);
var app = builder.Build();

app.Services.GetRequiredService<DataGenerator>().GenerateGames();

app.MapGrpcService<BulletinService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");


app.Run();
