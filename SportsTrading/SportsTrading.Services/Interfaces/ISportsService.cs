using SportsTrading.Data.Models;
using System.Linq;

namespace SportsTrading.Services.Interfaces
{
    public interface ISportsService
    {
        IQueryable<Event> GetEvents(int page, string search, int eventsPerPage);
    }
}
