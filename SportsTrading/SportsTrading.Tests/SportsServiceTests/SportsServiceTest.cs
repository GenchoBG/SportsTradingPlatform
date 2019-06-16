using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SportsTrading.Data;
using SportsTrading.Data.Models;
using SportsTrading.Services.Implementations;
using SportsTrading.Services.Interfaces;

namespace SportsTrading.Tests.SportsServiceTests
{
    public abstract class SportsServiceTest
    {
        protected ISportsService SportService { get; set; }

        private SportsTradingDbContext DbContext { get; }

        protected IList<Event> Events { get; set; }

        protected IList<Sport> Sports { get; set; }

        protected IList<League> Leagues { get; set; }
        
        protected SportsServiceTest()
        {
            this.DbContext = this.GetDbContext();

            this.Sports = new List<Sport>()
            {
                new Sport() { Name = "Grebane s lujica" },
                new Sport() { Name = "Pluvane" },
                new Sport() { Name = "Minavane v 12ti klas TUES" },
            };

            this.DbContext.Sports.AddRange(this.Sports);
            this.DbContext.SaveChanges();

            this.Leagues = new List<League>()
            {
                new League() { Name = "Svetovno bez vremevo ogranichenie", Sport = this.Sports[0] },
                new League() { Name = "Regionalno 2019", Sport = this.Sports[1] },
                new League() { Name = "TUES 2018/2019", Sport = this.Sports[2] }
            };

            this.DbContext.Leagues.AddRange(this.Leagues);
            this.DbContext.SaveChanges();

            this.Events = new List<Event>()
            {
                new Event() { AwayTeamScore = 1, AwayTeamOdds = 3, Date = DateTime.Now, HomeTeamOdds = 5, HomeTeamScore = 2, DrawOdds = 1, League = this.Leagues[0], Sport = this.Sports[0], Name = "Mitko vs Gencho" },
                new Event() { AwayTeamScore = 3, AwayTeamOdds = 1, Date = DateTime.Now, HomeTeamOdds = 7, HomeTeamScore = 1, DrawOdds = 1, League = this.Leagues[1], Sport = this.Sports[1], Name = "Pesho vs Gosho" },
                new Event() { AwayTeamScore = 1000, AwayTeamOdds = 1000, Date = DateTime.Now, HomeTeamOdds = 0, HomeTeamScore = 0, DrawOdds = 0, League = this.Leagues[2], Sport = this.Sports[2], Name = "Vsichki vs Gocev" },
            };

            this.DbContext.Events.AddRange(this.Events);
            this.DbContext.SaveChanges();

            this.SportService = new SportsService(this.DbContext);
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
