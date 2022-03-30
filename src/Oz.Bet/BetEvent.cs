namespace Oz.Bet
{
    public class BetEvent
    {
        public Event Event { get; set; }
        public Dictionary<MarketType, Market> Markets { get; set; }
    }
}
