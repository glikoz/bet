using Oz.Bet.Broker.Services;
using Xunit;

namespace Oz.Bet.Broker.Test
{
    public class BetSlipTests : Test
    {
        [Fact]
        public async void happy_path_for_betslip()
        {
            AccountService.CreateAccount("user:oguz");
            var bet = new BetSlipContext("web", "user:oguz", Markets.WHO_WINS, "game:1", "home", 10, 1.2m);
            await BetSlipService.CreateAsync(bet);

        }
    }
}
