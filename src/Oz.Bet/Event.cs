namespace Oz.Bet
{
    public class Event
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }

    public class Match : Event
    {
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public string Name
        {
            get { return $"{HomeTeam.Name}-{AwayTeam.Name}"; }
        }
    }
    public class Team
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
