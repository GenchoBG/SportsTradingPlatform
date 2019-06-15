using SportsTrading.Web.Models.Enums;

namespace SportsTrading.Web.Models
{
    public class StatisticViewModel
    {
        public StatisticType Type { get; set; }

        public string Name { get; set; }

        public int Count { get; set; }
    }
}
