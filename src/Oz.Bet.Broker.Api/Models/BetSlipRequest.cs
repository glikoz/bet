namespace Oz.Bet.Broker
{
    public class BetSlipRequest
    {

        public string GameId { get; set; }
        public string Market { get; set; }
        public string Outcome { get; set; }
        public decimal Amount { get; set; }
        public decimal Odd { get; set; }
    }
}