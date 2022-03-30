using Dapper;
using Oz.Bet.Broker.Command;
using System.Data;

namespace Oz.Bet.Broker.Repositories
{
    public class SqlAccountRepository : IAccountRepository
    {
        const string sql = @"UPDATE Account SET Balance=Balance-@Amount where UserId=@UserId and Balance-@Amount>=0
                                IF @@ROWCOUNT = 0 BEGIN 
                                       RETURN -1 
                                END
                                ELSE BEGIN
                                      SELECT Balance FROM Account where UserId=@UserId  
                                END";
        const string CREATE_ACCOUNT = @"INSERT INTO Account SET VALUES (@UserId,0)";
        private readonly IDbConnection dbConnection;

        public SqlAccountRepository(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public async Task ChangeBalance(ChangeBalanceCommand changeBalanceCommand)
        {
            await dbConnection.ExecuteAsync(sql, changeBalanceCommand);
        }

        public async Task CreateAccount(string userId)
        {
            await dbConnection.ExecuteAsync(CREATE_ACCOUNT, new { UserId = "user:oguz" });
        }
    }
}
