using System.ComponentModel.DataAnnotations;

namespace SportsTrading.Data.Models
{
    public class League
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string Name { get; set; }

        public int SportId { get; set; }

        [Required]
        public Sport Sport { get; set; }
    }
}
