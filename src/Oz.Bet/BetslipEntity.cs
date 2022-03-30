namespace Oz.Bet
{
    public class BetslipEntity
    {
        public string Id { get; }
        public string Channel { get; }
        public decimal Amount { get; }
        public string Game { get; }
        public string Market { get; }
        public string Outcome { get; }
        public decimal RequestedOdd { get; }
        public decimal ExecutionOdd { get; }
        public DateTime Timestamp { get; }
        public BetslipEntity(string id, string channel, string game, string market, string outcome, decimal requestedOdd, decimal executionOdd, decimal amount)
        {
            Id = id;
            Channel = channel;
            Game = game;
            Market = market;
            Outcome = outcome;
            RequestedOdd = requestedOdd;
            ExecutionOdd = executionOdd;
            Amount = amount;
            Timestamp = DateTime.UtcNow;
        }
    }
}
