﻿using SportsTrading.Data;
using SportsTrading.Data.Models;
using SportsTrading.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SportsTrading.Services.Implementations
{
    public class SportsService : ISportsService
    {
        private readonly SportsTradingDbContext db;

        public SportsService(SportsTradingDbContext db)
        {
            this.db = db;
        }

        public IQueryable<Event> GetEvents()
        {
            return this.db.Events.AsQueryable();
        }

        public IQueryable<Event> GetEvents(int page, string search, int eventsPerPage)
        {
            IQueryable<Event> eventsQuery = this.db.Events.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                eventsQuery =
                    eventsQuery.Where(e => e.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                                        || e.Sport.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                                        || e.League.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase));
            }

            eventsQuery = eventsQuery
                .Skip(page * eventsPerPage)
                .Take(eventsPerPage);

            return eventsQuery;
        }

        public async Task<int> GetCountAsync(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return await this.db.Events.CountAsync();
            }

            return await this.db.Events
                .CountAsync(e => e.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                                 || e.Sport.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                                 || e.League.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase));

        }

        public async Task<Event> GetEventAsync(int id)
        {
            return await this.db.Events
                .Include(e => e.Sport)
                .Include(e => e.League)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateOddsAsync(int id, decimal home, decimal away, decimal draw)
        {
            var @event = await this.GetEventAsync(id);

            if (@event != null)
            {
                @event.HomeTeamOdds = home;
                @event.AwayTeamOdds = away;
                @event.DrawOdds = draw;
            }

            await this.db.SaveChangesAsync();
        }
    }
}
