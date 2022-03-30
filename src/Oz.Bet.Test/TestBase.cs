using Microsoft.Extensions.DependencyInjection;
using Oz.Bet.DataProvider;
using System;

namespace Oz.Bet.Test
{
    public class Test : TestBase
    {
        protected IBulletinWriteRepository BulletinWriteRepository;
        protected override void RegisterServices(ServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<DataGenerator>();
            serviceCollection.AddScoped<IBulletinWriteRepository, RedisBulletinWriteRepository>();
            serviceCollection.AddStackExchangeRedisCache(config =>
            {
                config.Configuration = Environment.GetEnvironmentVariable("Redis");
            });
        }

        protected override void ResolveCommonServices()
        {
            BulletinWriteRepository = ServiceProvider.GetRequiredService<IBulletinWriteRepository>();
        }
    }
}
