using Dapper.Contrib.Extensions;
using System.Data;
namespace Oz.Bet.Broker.Repositories
{
    public class SqlBetslipRepository : IBetslipRepository
    {
        private readonly IDbConnection dbConnection;

        public SqlBetslipRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public async Task CreateAsync(BetslipEntity entity)
        {
            await dbConnection.InsertAsync(entity);
        }
    }
}
