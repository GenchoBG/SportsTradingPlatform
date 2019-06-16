using SportsTrading.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SportsTrading.Services.Interfaces
{
    public interface ISportsService
    {
        IQueryable<Event> GetEvents();
        IQueryable<Event> GetEvents(int page, string search, int eventsPerPage);
        Task<int> GetCountAsync(string search);
        Task<Event> GetEventAsync(int id);
    }
}
