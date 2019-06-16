using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SportsTrading.Tests.SportsServiceTests
{
    public class UpdateOddsTests : SportsServiceTest
    {
        [Fact]
        public async Task TestUpdateEvent()
        {
            var @event = this.Events.First();

            await this.SportService.UpdateOddsAsync(@event.Id, 69, 69, 69);

            Assert.Equal(expected: 69, actual: @event.HomeTeamOdds);
            Assert.Equal(expected: 69, actual: @event.AwayTeamOdds);
            Assert.Equal(expected: 69, actual: @event.DrawOdds);

            @event = this.Events.Last();

            await this.SportService.UpdateOddsAsync(@event.Id, 69, 69, 69);

            Assert.Equal(expected: 69, actual: @event.HomeTeamOdds);
            Assert.Equal(expected: 69, actual: @event.AwayTeamOdds);
            Assert.Equal(expected: 69, actual: @event.DrawOdds);
        }
    }
}
