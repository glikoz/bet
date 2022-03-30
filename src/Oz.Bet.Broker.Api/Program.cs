using MassTransit;
using Oz.Bet;
using Oz.Bet.Broker.Repositories;
using Oz.Bet.Broker.Services;
using Oz.Bet.Data;
using System.Data;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<BetslipService>();
builder.Services.AddScoped<IAccountRepository, SqlAccountRepository>();
builder.Services.AddScoped<IBetslipRepository, SqlBetslipRepository>();
builder.Services.AddScoped<IDbConnection, SqlConnection>(p =>
{
    var conn = new SqlConnection(Environment.GetEnvironmentVariable("Sql"));
    conn.Open();
    return conn;
});
builder.Services.AddScoped<IBulletinReadRepository, RedisBulletinReadRepository>();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(Environment.GetEnvironmentVariable("RabbitUrl"));
        cfg.ConfigureEndpoints(ctx);
    });
});
builder.Services.AddOptions<MassTransitHostOptions>()
    .Configure(options =>
    {
        options.WaitUntilStarted = true;
    });

builder.Services.AddStackExchangeRedisCache(config =>
{
    config.Configuration = Environment.GetEnvironmentVariable("Redis");
});

LogHelper.Init(builder.Services);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
