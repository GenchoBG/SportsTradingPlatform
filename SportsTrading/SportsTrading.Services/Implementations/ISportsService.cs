using System.Linq;
using SportsTrading.Data.Models;

namespace SportsTrading.Services.Implementations
{
    public interface ISportsService
    {
        IQueryable<Event> GetEvents(int page = 0, string search = "", int eventsPerPage = 20);
    }
}
