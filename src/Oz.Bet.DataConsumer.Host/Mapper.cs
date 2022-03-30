using BetProto;

namespace Oz.Bet.DataConsumer.Host
{
    internal static class Mapper
    {
        internal static BulletinEntity Map(BulletinResult bulletinResult)
        {
            var bulletin = new BulletinEntity
            {
                Version = bulletinResult.VersionId,
            };

            foreach (var game in bulletinResult.GameList)
            {
                var gameEntity = new GameEntity(game.Id);
                gameEntity.Name = game.Name;
                var market = new Market
                {
                    Key = Markets.WHO_WINS
                };

                market.OutcomeDict.Add(Outcomes.HOME, new Outcome { Key = Outcomes.HOME, Odd = (decimal)game.Home });
                market.OutcomeDict.Add(Outcomes.DRAW, new Outcome { Key = Outcomes.DRAW, Odd = (decimal)game.Draw });
                market.OutcomeDict.Add(Outcomes.AWAY, new Outcome { Key = Outcomes.AWAY, Odd = (decimal)game.Away });
                gameEntity.MarketDict.Add(market.Key, market);

                bulletin.Games.Add(gameEntity.Id, gameEntity);
            }

            return bulletin;
        }
    }
}
