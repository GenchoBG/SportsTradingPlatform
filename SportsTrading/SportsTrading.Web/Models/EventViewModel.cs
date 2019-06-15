using AutoMapper;
using SportsTrading.Data.Models;
using SportsTrading.Web.Infrastructure.Mapper;

namespace SportsTrading.Web.Models
{
    public class EventViewModel : ICustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LeagueName { get; set; }

        public string SportName { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile
                .CreateMap<Event, EventViewModel>()
                .ForMember(m => m.LeagueName, opts => opts.MapFrom(e => e.League.Name))
                .ForMember(m => m.SportName, opts => opts.MapFrom(e => e.Sport.Name));
        }
    }
}
