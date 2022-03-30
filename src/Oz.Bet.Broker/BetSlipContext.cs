namespace Oz.Bet.Broker
{
    public class BetSlipContext
    {
        public BetSlipContext(string channel, string userId, string gameId, string market, string outcome, decimal amount, decimal odd)
        {
            Channel = channel ?? throw new ArgumentNullException(nameof(channel)); ;
            UserId = userId ?? throw new ArgumentNullException(nameof(userId));
            GameId = gameId ?? throw new ArgumentNullException(nameof(gameId));
            Outcome = outcome ?? throw new ArgumentNullException(nameof(outcome));
            Market = market ?? throw new ArgumentNullException(nameof(market));
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount));
            if (odd <= 1)
                throw new ArgumentOutOfRangeException(nameof(odd));
            Amount = amount;
            Odd = odd;
            Id = MassTransit.NewId.Next().ToString();
            Timestamp = DateTime.UtcNow;
        }

        public string Channel { get; }
        public string Market { get; }
        public string Outcome { get; }
        public string UserId { get; }
        public DateTime Timestamp { get; }
        public string Id { get; }
        public string GameId { get; }
        public decimal Amount { get; }
        public decimal Odd { get; }
    }
}
