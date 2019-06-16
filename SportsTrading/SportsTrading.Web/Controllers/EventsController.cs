using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsTrading.Services.Interfaces;
using SportsTrading.Web.Models;
using System.Threading.Tasks;
using AutoMapper;

namespace SportsTrading.Web.Controllers
{
    public class EventsController : Controller
    {
        private readonly ISportsService sportsService;
        private readonly IMapper mapper;

        public EventsController(ISportsService sportsService, IMapper mapper)
        {
            this.sportsService = sportsService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        [ResponseCache(VaryByQueryKeys = new[] { "search", "page", "eventsPerPage"  }, Duration = 30)]
        public async Task<IActionResult> GetEvents(string search, int page, int eventsPerPage = 20)
        {
            return this.Json(await this.sportsService.GetEvents(page, search, eventsPerPage).ProjectTo<EventListViewModel>().ToListAsync());
        }

        [HttpGet]
        [ResponseCache(VaryByQueryKeys = new[] { "search" }, Duration = 30)]
        public async Task<IActionResult> GetEventsCount(string search)
        {
            return this.Json(await this.sportsService.GetCountAsync(search));
        }

        [HttpGet]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetEventsPerLeagueStatistics()
        {
            return this.Json(await this.sportsService.GetEvents()
                                    .GroupBy(e => e.League.Name)
                                    .ToDictionaryAsync(grouping => grouping.Key, grouping => grouping.Count()));
        }

        [HttpGet]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetEventsPerSportStatistics()
        {
            return this.Json(await this.sportsService.GetEvents()
                .GroupBy(e => e.Sport.Name)
                .ToDictionaryAsync(grouping => grouping.Key, grouping => grouping.Count()));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var @event = await this.sportsService.GetEventAsync(id);

            if (@event == null)
            {
                return this.BadRequest();
            }

            var model = this.mapper.Map<EventDetailsViewModel>(@event);

            return this.View(model);
        }
    }
}
