namespace Oz.Bet
{
    public class Market
    {
        public string Key { get; set; }
        public string SpecialBetValue { get; set; }
        public int Version { get; set; }
        public Dictionary<string, Outcome> OutcomeDict { get; set; } = new();

    }
    public class Outcome
    {
        public string Key { get; set; }
        public decimal Odd { get; set; }
    }
}
