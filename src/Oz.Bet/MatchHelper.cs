namespace Oz.Bet
{
    public static class MatchHelper
    {
        public static string GetMatchName(Team home, Team away)
        {
            return $"{home.Name} {away.Name}";
        }
    }
}
