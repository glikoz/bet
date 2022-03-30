using Microsoft.Extensions.Caching.Distributed;
using Oz.Bet.Exceptions;
using System.Text.Json;

namespace Oz.Bet.Data
{
    public class RedisBulletinReadRepository : IBulletinReadRepository
    {
        private readonly IDistributedCache cache;

        public RedisBulletinReadRepository(IDistributedCache cache)
        {
            this.cache = cache;
        }
        public async Task<BulletinEntity> GetBulletinAsync()
        {
            var bulletin = await cache.GetStringAsync(RedisKeys.Bulletin);
            ArgumentNullException.ThrowIfNull(bulletin, nameof(bulletin));

            var res = JsonSerializer.Deserialize<BulletinEntity>(bulletin);
            if (res == null)
                throw new DomainException("Bulletin cannot be deserialized");
            return res;
        }
    }
}
