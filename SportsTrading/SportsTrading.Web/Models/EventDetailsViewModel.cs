using System;
using AutoMapper;
using SportsTrading.Data.Models;
using SportsTrading.Web.Infrastructure.Mapper;

namespace SportsTrading.Web.Models
{
    public class EventDetailsViewModel : ICustomMapping
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public DateTime Date { get; set; }

        public string LeagueName { get; set; }

        public string SportName { get; set; }

        public int HomeTeamScore { get; set; }

        public int AwayTeamScore { get; set; }

        public decimal HomeTeamOdds { get; set; }

        public decimal AwayTeamOdds { get; set; }

        public decimal DrawOdds { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile
                .CreateMap<Event, EventDetailsViewModel>()
                .ForMember(m => m.LeagueName, opts => opts.MapFrom(e => e.League.Name))
                .ForMember(m => m.SportName, opts => opts.MapFrom(e => e.Sport.Name));
        }
    }
}
