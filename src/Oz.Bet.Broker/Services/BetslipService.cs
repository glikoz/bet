using MassTransit;
using Oz.Bet.Broker.Command;
using Oz.Bet.Broker.Repositories;
using Oz.Bet.Data;
using Oz.Bet.Exceptions;

namespace Oz.Bet.Broker.Services
{
    public class BetslipService
    {
        private const string REASON = "PlaceBet";
        private readonly IPublishEndpoint publishEndpoint;
        private readonly AccountService accountService;
        private readonly IBulletinReadRepository bulletinReadRepository;
        private readonly IBetslipRepository betslipRepository;

        public BetslipService(IPublishEndpoint publishEndpoint, AccountService accountService, IBulletinReadRepository bulletinReadRepository, IBetslipRepository betslipRepository)
        {
            this.publishEndpoint = publishEndpoint;
            this.accountService = accountService;
            this.bulletinReadRepository = bulletinReadRepository;
            this.betslipRepository = betslipRepository;
        }

        public async Task<ServiceResult<string>> CreateAsync(BetSlipContext betSlipContext)
        {
            accountService.ChangeBalance(new ChangeBalanceCommand(betSlipContext.UserId, betSlipContext.Amount, REASON));
            var bulletin = await bulletinReadRepository.GetBulletinAsync();
            if (!bulletin.Games.TryGetValue(betSlipContext.GameId, out GameEntity game))
                throw new DomainException("Game does not exists");

            if (!game.MarketDict.TryGetValue(betSlipContext.Market, out Market market))
                throw new DomainException("Market is closed for this event");

            if (!market.OutcomeDict.TryGetValue(betSlipContext.Outcome, out Outcome outcome))
                throw new DomainException("Outcome is closed for this market");

            var entity = new BetslipEntity(betSlipContext.Id, betSlipContext.Channel, betSlipContext.GameId, betSlipContext.Market, betSlipContext.Outcome, betSlipContext.Odd, outcome.Odd, betSlipContext.Amount);

            await betslipRepository.CreateAsync(entity);

            await publishEndpoint.Publish(entity);

            return new ServiceResult<string>() { Result = betSlipContext.Id };
        }
    }
}
