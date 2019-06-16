using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SportsTrading.Data;
using SportsTrading.Data.Models;
using SportsTrading.Services.Implementations;
using SportsTrading.Services.Interfaces;
using Xunit;

namespace SportsTrading.Tests
{
    public class SportServiceTests
    {
        private ISportsService sportService;

        private IList<Event> Events { get; set; }

        private IList<Sport> Sports { get; set; }

        private IList<League> Leagues { get; set; }

        public SportServiceTests()
        {
            var db = this.GetDbContext();

            this.Sports = new List<Sport>()
            {
                new Sport() { Name = "Grebane s lujica" },
                new Sport() { Name = "Pluvane" },
                new Sport() { Name = "Minavane v 12ti klas TUES" },
            };

            db.Sports.AddRange(this.Sports);
            db.SaveChanges();

            this.Leagues = new List<League>()
            {
                new League() { Name = "Svetovno bez vremevo ogranichenie", Sport = this.Sports[0] },
                new League() { Name = "Regionalno 2019", Sport = this.Sports[1] },
                new League() { Name = "TUES 2018/2019", Sport = this.Sports[2] }
            };

            db.Leagues.AddRange(this.Leagues);
            db.SaveChanges();

            this.Events = new List<Event>()
            {
                new Event() { AwayTeamScore = 1, AwayTeamOdds = 3, Date = DateTime.Now, HomeTeamOdds = 5, HomeTeamScore = 2, DrawOdds = 1, League = this.Leagues[0], Sport = this.Sports[0], Name = "Mitko vs Gencho" },
                new Event() { AwayTeamScore = 3, AwayTeamOdds = 1, Date = DateTime.Now, HomeTeamOdds = 7, HomeTeamScore = 1, DrawOdds = 1, League = this.Leagues[1], Sport = this.Sports[1], Name = "Pesho vs Gosho" },
                new Event() { AwayTeamScore = 1000, AwayTeamOdds = 1000, Date = DateTime.Now, HomeTeamOdds = 0, HomeTeamScore = 0, DrawOdds = 0, League = this.Leagues[2], Sport = this.Sports[2], Name = "Vsichki vs Gocev" },
            };

            db.Events.AddRange(this.Events);
            db.SaveChanges();

            this.sportService = new SportsService(db);
        }

        [Fact]
        public void TestGetEventsWithoutParams()
        {
            var actual = this.sportService.GetEvents().ToList();

            Assert.True(this.Events.SequenceEqual(actual));
        }

        [Fact]
        public void TestGetEventsWithPagination()
        {
            var page = 0;
            var perPage = 2;

            var actual = this.sportService.GetEvents(page: page, eventsPerPage: perPage, search: "").ToList();

            Assert.True(this.Events.Skip(page * perPage).Take(perPage).SequenceEqual(actual));

            page = 1;

            actual = this.sportService.GetEvents(page: page, eventsPerPage: perPage, search: "").ToList();

            Assert.True(this.Events.Skip(page * perPage).Take(perPage).SequenceEqual(actual));
        }

        [Fact]
        public void TestGetEventsWithPaginationAndSearch()
        {
            var page = 0;
            var perPage = 20;
            var search = "TUES";

            var actual = this.sportService.GetEvents(page: page, eventsPerPage: perPage, search: search).ToList();

            Assert.True(this.Events
                            .Skip(page * perPage)
                            .Take(perPage)
                            .Where(e => e.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                                                           || e.Sport.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                                                           || e.League.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)).SequenceEqual(actual));

            search = "gocev";

            actual = this.sportService.GetEvents(page: page, eventsPerPage: perPage, search: search).ToList();

            Assert.True(this.Events
                .Skip(page * perPage)
                .Take(perPage)
                .Where(e => e.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                            || e.Sport.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                            || e.League.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)).SequenceEqual(actual));

            search = "pluvane";

            actual = this.sportService.GetEvents(page: page, eventsPerPage: perPage, search: search).ToList();

            Assert.True(this.Events
                .Skip(page * perPage)
                .Take(perPage)
                .Where(e => e.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                            || e.Sport.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                            || e.League.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)).SequenceEqual(actual));

            search = "2018";

            actual = this.sportService.GetEvents(page: page, eventsPerPage: perPage, search: search).ToList();

            Assert.True(this.Events
                .Skip(page * perPage)
                .Take(perPage)
                .Where(e => e.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                            || e.Sport.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                            || e.League.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)).SequenceEqual(actual));

        }

        private SportsTradingDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<SportsTradingDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var db = new SportsTradingDbContext(options);

            return db;
        }

    }
}
