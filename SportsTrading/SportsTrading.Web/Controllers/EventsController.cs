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
        public async Task<IActionResult> GetEvents(string search, int page, int eventsPerPage = 20)
        {
            return this.Json(await this.sportsService.GetEvents(page, search, eventsPerPage).ProjectTo<EventListViewModel>().ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetEventsCount(string search)
        {
            return this.Json(await this.sportsService.GetCountAsync(search));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var @event = await this.sportsService.GetEventAsync(id);

            var model = this.mapper.Map<EventDetailsViewModel>(@event);

            return this.View(model);
        }
    }
}
