using Oz.Bet.Broker.Command;
using Oz.Bet.Broker.Repositories;

namespace Oz.Bet.Broker.Services
{
    public class AccountService
    {
        public AccountService(IAccountRepository accountRepository)
        {
            AccountRepository = accountRepository;
        }

        public IAccountRepository AccountRepository { get; }

        public void ChangeBalance(ChangeBalanceCommand changeBalanceCommand)
        {
            AccountRepository.ChangeBalance(changeBalanceCommand);
        }

        public void CreateAccount(string userId)
        {
            AccountRepository.CreateAccount(userId);
        }
    }
}
