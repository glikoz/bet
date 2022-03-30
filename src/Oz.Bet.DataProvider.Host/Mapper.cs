using BetProto;

namespace Oz.Bet.DataProvider.Host
{
    public static class Mapper
    {
        public static BulletinResult Map(BulletinEntity bulletinEntity)
        {
            var res = new BulletinResult
            {
                VersionId = bulletinEntity.Version
            };
            res.GameList.Clear();
            res.GameList.Add(bulletinEntity.Games.Values.Select(p => new Game
            {
                Name = p.Name,
                Id = p.Id,
                Home = (double)p.MarketDict[Markets.WHO_WINS].OutcomeDict[Outcomes.HOME].Odd,
                Away = (double)p.MarketDict[Markets.WHO_WINS].OutcomeDict[Outcomes.AWAY].Odd,
                Draw = (double)p.MarketDict[Markets.WHO_WINS].OutcomeDict[Outcomes.DRAW].Odd
            }));

            return res;
        }
    }
}
