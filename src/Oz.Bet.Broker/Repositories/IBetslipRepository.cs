namespace Oz.Bet.Broker.Repositories
{
    public interface IBetslipRepository
    {
        Task CreateAsync(BetslipEntity entity);
    }
}
