using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SportsTrading.Tests.SportsServiceTests
{
    public class CountEventsTests : SportsServiceTest
    {
        [Fact]
        public async Task TestGetEventsWithoutSearch()
        {
            var actual = await this.SportService.GetCountAsync("");

            Assert.True(this.Events.Count.Equals(actual));
        }

        [Fact]
        public async Task TestGetEventsWithPaginationAndSearch()
        {
            var search = "TUES";

            var actual = await this.SportService.GetCountAsync(search);

            Assert.True(this.Events.Count(e => e.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                                                           || e.Sport.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                                                           || e.League.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)).Equals(actual));

            search = "gocev";

            actual = await this.SportService.GetCountAsync(search);

            Assert.True(this.Events.Count(e => e.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                                               || e.Sport.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                                               || e.League.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)).Equals(actual));

            search = "pluvane";

            actual = await this.SportService.GetCountAsync(search);

            Assert.True(this.Events.Count(e => e.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                                               || e.Sport.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                                               || e.League.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)).Equals(actual));

            search = "2018";

            actual = await this.SportService.GetCountAsync(search);

            Assert.True(this.Events.Count(e => e.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                                               || e.Sport.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                                               || e.League.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase)).Equals(actual));
        }
    }
}
