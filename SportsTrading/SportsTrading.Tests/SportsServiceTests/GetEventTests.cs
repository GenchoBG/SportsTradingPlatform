using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SportsTrading.Tests.SportsServiceTests
{
    public class GetEventTests : SportsServiceTest
    {
        [Fact]
        public async Task TestGetEvent()
        {
            var actual = await this.SportService.GetEventAsync(this.Events.First().Id);

            Assert.True(this.Events.First().Equals(actual));

            actual = await this.SportService.GetEventAsync(this.Events.Last().Id);

            Assert.True(this.Events.Last().Equals(actual));
        }
    }
}
