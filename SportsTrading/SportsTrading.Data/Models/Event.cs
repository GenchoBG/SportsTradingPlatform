using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsTrading.Data.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string Name { get; set; }

        public int SportId { get; set; }

        [Required]
        [ForeignKey(nameof(SportId))]
        public Sport Sport { get; set; }

        public int LeagueId { get; set; }

        [Required]
        [ForeignKey(nameof(LeagueId))]
        public League League { get; set; }

        public int HomeTeamScore { get; set; }

        public int AwayTeamScore { get; set; }

        public decimal HomeTeamOdds { get; set; }

        public decimal AwayTeamOdds { get; set; }
    }
}
