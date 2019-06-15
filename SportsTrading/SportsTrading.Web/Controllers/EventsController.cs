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

        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents(string search, int page, int eventsPerPage = 20)
        {
            return this.Json(await this.sportsService.GetEvents(page, search, eventsPerPage).ProjectTo<EventListViewModel>().ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetEventsCount(string search, int page, int eventsPerPage = 20)
        {
            return this.Json(await this.sportsService.GetEvents(page, search, eventsPerPage).ProjectTo<EventListViewModel>().CountAsync());
        }
    }
}
