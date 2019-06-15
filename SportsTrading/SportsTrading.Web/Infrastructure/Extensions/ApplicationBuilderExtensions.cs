using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SportsTrading.Data;
using SportsTrading.Data.Models;
using SportsTrading.Web.Infrastructure.Serialization;

namespace SportsTrading.Web.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder Seed(this IApplicationBuilder app)
        {
            var jsonPath = @"sport-events.json";
            var json = File.ReadAllText(jsonPath);
            var events = JsonConvert.DeserializeObject<List<Serialization.Event>>(json);

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<SportsTradingDbContext>().Database.Migrate();

                var db = serviceScope.ServiceProvider.GetService<SportsTradingDbContext>();

                Task
                    .Run(async () =>
                    {
                        if (!await db.Sports.AnyAsync())
                        {
                            var sports = new Dictionary<int, Serialization.Sport>();
                            var leagues = new Dictionary<int, Serialization.League>();
                            foreach (var @event in events)
                            {
                                if (!sports.ContainsKey(@event.Sport.Id))
                                {
                                    sports[@event.Sport.Id] = @event.Sport;
                                }
                                if (!leagues.ContainsKey(@event.League.Id))
                                {
                                    leagues[@event.League.Id] = @event.League;
                                }
                            }

                            foreach (var sport in sports.Values)
                            {
                                db.Sports.Add(new SportsTrading.Data.Models.Sport()
                                {
                                    Name = sport.Name,
                                    Id = sport.Id
                                });
                            }
                            using (var transaction = db.Database.BeginTransaction())
                            {
                                db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Sports ON");
                                db.SaveChanges();
                                db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Sports OFF");
                                transaction.Commit();
                            }

                            foreach (var league in leagues.Values)
                            {
                                db.Leagues.Add(new SportsTrading.Data.Models.League()
                                {
                                    Name = league.Name,
                                    Id = league.Id,
                                    SportId = league.SportId
                                });
                            }
                            using (var transaction = db.Database.BeginTransaction())
                            {
                                db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Leagues ON");
                                db.SaveChanges();
                                db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Leagues OFF");
                                transaction.Commit();
                            }

                            foreach (var @event in events)
                            {
                                db.Events.Add(new SportsTrading.Data.Models.Event()
                                {
                                    Name = @event.EventName,
                                    Date = @event.EventDate,
                                    SportId = @event.Sport.Id,
                                    LeagueId = @event.League.Id,
                                    AwayTeamOdds = @event.AwayTeamOdds,
                                    HomeTeamOdds = @event.HomeTeamOdds,
                                    DrawOdds = @event.DrawOdds,
                                    AwayTeamScore = @event.AwayTeamScore,
                                    HomeTeamScore = @event.HomeTeamScore
                                });
                            }

                            db.SaveChanges();
                        }

                    })
                    .Wait();
            }

            return app;
        }
    }
}
