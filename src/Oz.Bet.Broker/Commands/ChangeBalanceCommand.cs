namespace Oz.Bet.Broker.Command
{
    public record ChangeBalanceCommand(string UserId, decimal Amount, string Reason);

}
