using MassTransit;
using Microsoft.Extensions.Logging;

namespace Oz.Bet.DataProvider
{
    public class DataGenerator
    {
        public DataGenerator(ILogger<DataGenerator> logger)
        {
            this.logger = logger;
        }
        public BulletinEntity Bulletin { get; private set; } = new BulletinEntity() { Version = 1 };

        readonly string[] TEAMS = new[] { "Liverpool", "Barcelona", "Galatasaray", "Celtic", "Milan" };
        private readonly ILogger<DataGenerator> logger;

        public void GenerateGames()
        {
            logger.LogInformation("Bulletin initialized");
            var comb = CombinationsHelper.Combinations(TEAMS, 2);

            var market = new Market { Key = Markets.WHO_WINS };
            market.OutcomeDict.Add(Outcomes.HOME, new Outcome { Key = Outcomes.HOME, Odd = 1.8m });
            market.OutcomeDict.Add(Outcomes.AWAY, new Outcome { Key = Outcomes.AWAY, Odd = 1.8m });
            market.OutcomeDict.Add(Outcomes.DRAW, new Outcome { Key = Outcomes.DRAW, Odd = 1.8m });

            var games = comb.Take(5).Select(p => new GameEntity
            {
                Name = $"{p.First()} - {p.Last()}",
                MarketDict = new Dictionary<string, Market>
            { {market.Key,market } },
                Version = 1
            }).ToDictionary(p => p.Id);

            Bulletin.Games = games;
        }

        public void ChangeOddsRandomly()
        {
            var index = Random.Shared.Next(0, 4);
            var oddChange = Random.Shared.Next(-10, 10);
            if (oddChange == 0)
                oddChange = 1;
            Bulletin.Change(index, oddChange);
        }

    }
}