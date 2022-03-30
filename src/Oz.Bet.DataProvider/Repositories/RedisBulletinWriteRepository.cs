using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Oz.Bet.DataProvider
{
    public class RedisBulletinWriteRepository : IBulletinWriteRepository
    {
        private readonly IDistributedCache cache;

        public RedisBulletinWriteRepository(IDistributedCache cache)
        {
            this.cache = cache;
        }
        public async Task ReplaceAsync(BulletinEntity bulletinEntity)
        {
            var val = JsonSerializer.Serialize(bulletinEntity);
            await cache.SetStringAsync(RedisKeys.Bulletin, val);
        }
    }
}
