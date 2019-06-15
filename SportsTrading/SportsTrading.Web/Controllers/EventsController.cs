using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsTrading.Services.Implementations;
using SportsTrading.Services.Interfaces;
using SportsTrading.Web.Models;

namespace SportsTrading.Web.Controllers
{
    public class EventsController : Controller
    {
        private readonly ISportsService sportsService;

        public EventsController(ISportsService sportsService)
        {
            this.sportsService = sportsService;
        }

        public async Task<IActionResult> Index(int page = 0, string search = "", int eventsPerPage = 20)
        {
            var events = await this.sportsService
                .GetEvents(page, search, eventsPerPage)
                .ProjectTo<EventViewModel>()
                .ToListAsync();

            return this.View(events);
        }
    }
}
