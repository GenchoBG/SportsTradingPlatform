using System;
using System.Linq;
using SportsTrading.Tests.SportsServiceTests;
using Xunit;

namespace SportsTrading.Tests
{
    public class GetEventsTests : SportsServiceTest
    {
        [Fact]
        public void TestGetEventsWithoutParams()
        {
            var actual = this.SportService.GetEvents().ToList();

            Assert.True(this.Events.SequenceEqual(actual));
        }

        [Fact]
        public void TestGetEventsWithPagination()
        {
            var page = 0;
            var perPage = 2;

            var actual = this.SportService.GetEvents(page: page, eventsPerPage: perPage, search: "").ToList();

            Assert.True(this.Events.Skip(page * perPage).Take(perPage).SequenceEqual(actual));

            page = 1;

            actual = this.SportService.GetEvents(page: page, eventsPerPage: perPage, search: "").ToList();

            Assert.True(this.Events.Skip(page * perPage).Take(perPage).SequenceEqual(actual));
        }

        [Fact]
        public void TestGetEventsWithPaginationAndSearch()
        {
            var page = 0;
            var perPage = 20;
            var search = "TUES";

            var actual = this.SportService.GetEvents(page: page, eventsPerPage: perPage, search: search).ToList();

            Assert.True(this.Events
                            .Skip(page * perPage)
                            .Take(perPage)
                            .Where(e => e.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                                                           || e.Sport.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                                                           || e.League.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)).SequenceEqual(actual));

            search = "gocev";

            actual = this.SportService.GetEvents(page: page, eventsPerPage: perPage, search: search).ToList();

            Assert.True(this.Events
                .Skip(page * perPage)
                .Take(perPage)
                .Where(e => e.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                            || e.Sport.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                            || e.League.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)).SequenceEqual(actual));

            search = "pluvane";

            actual = this.SportService.GetEvents(page: page, eventsPerPage: perPage, search: search).ToList();

            Assert.True(this.Events
                .Skip(page * perPage)
                .Take(perPage)
                .Where(e => e.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                            || e.Sport.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                            || e.League.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)).SequenceEqual(actual));

            search = "2018";

            actual = this.SportService.GetEvents(page: page, eventsPerPage: perPage, search: search).ToList();

            Assert.True(this.Events
                .Skip(page * perPage)
                .Take(perPage)
                .Where(e => e.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                            || e.Sport.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                            || e.League.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)).SequenceEqual(actual));

        }
    }
}
