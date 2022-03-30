using Oz.Bet.Broker.Command;

namespace Oz.Bet.Broker.Repositories
{
    public interface IAccountRepository
    {
        Task ChangeBalance(ChangeBalanceCommand changeBalanceCommand);
        Task CreateAccount(string userId);
    }


}
