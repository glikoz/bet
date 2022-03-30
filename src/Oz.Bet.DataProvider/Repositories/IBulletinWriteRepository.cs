namespace Oz.Bet.DataProvider
{
    public interface IBulletinWriteRepository
    {
        Task ReplaceAsync(BulletinEntity bulletinEntity);
    }
}
