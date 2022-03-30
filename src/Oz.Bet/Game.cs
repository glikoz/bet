using MassTransit;

namespace Oz.Bet
{
    public class GameEntity
    {
        public GameEntity()
        {
            Id = NewId.Next().ToString();
        }
        public GameEntity(string id)
        {
            Id = id;
        }
        public string Id { get; private set; }
        public string Name { get; set; }
        public Dictionary<string, Market> MarketDict { get; set; } = new Dictionary<string, Market>();
        public int Version { get; set; }

        public void ChangeOdds(int change)
        {
            var oddChange = change * 0.1m;
            var market = MarketDict[Markets.WHO_WINS];
            market.OutcomeDict[Outcomes.HOME].Odd += oddChange;
            market.OutcomeDict[Outcomes.DRAW].Odd += oddChange;
            market.OutcomeDict[Outcomes.AWAY].Odd += oddChange;

            if (market.OutcomeDict[Outcomes.HOME].Odd < 1.1m)
                market.OutcomeDict[Outcomes.HOME].Odd = 1.1m;
            if (market.OutcomeDict[Outcomes.DRAW].Odd < 1.1m)
                market.OutcomeDict[Outcomes.DRAW].Odd = 1.1m;
            if (market.OutcomeDict[Outcomes.AWAY].Odd < 1.1m)
                market.OutcomeDict[Outcomes.AWAY].Odd = 1.1m;

            Version++;
        }
    }

    public class BulletinEntity
    {
        public int Version { get; set; }
        public Dictionary<string, GameEntity> Games { get; set; } = new();

        public void Change(int index, int change)
        {
            var keys = Games.Keys.ToArray();
            Games[keys[index]].ChangeOdds(change);
            Version++;
        }
    }


}
