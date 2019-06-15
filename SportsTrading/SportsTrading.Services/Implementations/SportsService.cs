using SportsTrading.Data;
using SportsTrading.Data.Models;
using SportsTrading.Services.Interfaces;
using System;
using System.Linq;

namespace SportsTrading.Services.Implementations
{
    public class SportsService : ISportsService
    {
        private readonly SportsTradingDbContext db;

        public SportsService(SportsTradingDbContext db)
        {
            this.db = db;
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
    }
}
