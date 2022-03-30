using Microsoft.Extensions.DependencyInjection;
using Oz.Bet.Broker.Repositories;
using Oz.Bet.Broker.Services;
using Oz.Bet.Data;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Oz.Bet.Broker.Test
{
    public class Test : TestBase
    {
        protected BetslipService BetSlipService;
        protected AccountService AccountService;
        protected override void RegisterServices(ServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<AccountService>();
            serviceCollection.AddScoped<BetslipService>();
            serviceCollection.AddScoped<IBulletinReadRepository, RedisBulletinReadRepository>();
            serviceCollection.AddScoped<IAccountRepository, SqlAccountRepository>();

            serviceCollection.AddStackExchangeRedisCache(config =>
            {
                config.Configuration = Environment.GetEnvironmentVariable("Redis");
            });

            serviceCollection.AddScoped<IDbConnection, SqlConnection>(p =>
            {
                var conn = new SqlConnection(Environment.GetEnvironmentVariable("Sql"));
                conn.Open();
                return conn;
            });

        }
        protected override void ResolveCommonServices()
        {
            BetSlipService = ServiceProvider.GetRequiredService<BetslipService>();
            AccountService = ServiceProvider.GetRequiredService<AccountService>();
        }
    }
}