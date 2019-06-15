using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsTrading.Services.Interfaces;
using SportsTrading.Web.Models;
using System.Threading.Tasks;

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
            System.Collections.Generic.List<EventViewModel> events = await this.sportsService
                .GetEvents(page, search, eventsPerPage)
                .ProjectTo<EventViewModel>()
                .ToListAsync();

            return this.View(events);
        }
    }
}
