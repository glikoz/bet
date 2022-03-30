namespace Oz.Bet.Data
{
    public interface IBulletinReadRepository
    {
        Task<BulletinEntity> GetBulletinAsync();
    }
}
