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

        /// <summary>
        /// Gets a number of events based on a search string and a page number
        /// </summary>
        /// <param name="search">The search string</param>   
        /// <param name="page">The page which to take (skip (page * eventsPerPage) elements)</param>    
        /// <param name="eventsPerPage">How many events should a page contain</param>
        [HttpGet]
        [ResponseCache(VaryByQueryKeys = new[] { "search", "page", "eventsPerPage" }, Duration = 30)]
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
                .OrderByDescending(g => g.Key)
                .ToDictionaryAsync(grouping => grouping.Key, grouping => grouping.Count()));
        }

        [HttpGet]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> GetEventsPerSportStatistics()
        {
            return this.Json(await this.sportsService.GetEvents()
                .GroupBy(e => e.Sport.Name)
                .OrderByDescending(g => g.Key)
                .ToDictionaryAsync(grouping => grouping.Key, grouping => grouping.Count()));
        }

        public IActionResult Stats()
        {
            return this.View();
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

        [HttpPut]
        public async Task<IActionResult> UpdateOdds(int id, [FromBody]OddsBindingModel model)
        {
            var @event = await this.sportsService.GetEventAsync(id);

            if (@event == null)
            {
                return this.BadRequest();
            }

            await this.sportsService.UpdateOddsAsync(id, model.Home, model.Away, model.Draw);

            return this.Ok(new { id, model.Home, model.Away, model.Draw });
        }
    }
}
