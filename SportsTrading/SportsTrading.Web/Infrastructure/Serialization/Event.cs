using System;

namespace SportsTrading.Web.Infrastructure.Serialization
{
    public class Event
    {
        public string EventName { get; set; }

        public DateTime EventDate { get; set; }

        public League League { get; set; }

        public Sport Sport { get; set; }

        public int HomeTeamScore { get; set; }

        public int AwayTeamScore { get; set; }

        public decimal HomeTeamOdds { get; set; }

        public decimal AwayTeamOdds { get; set; }

        public decimal DrawOdds { get; set; }
    }
}
