using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace UnitTestRefactoring.Tests.Refactored.ReportingServiceTests
{
    public class GettingTotalVehicleCountsShould : ReportingServiceTestsBase
    {
        [Fact]
        public async Task ReturnTotalVehicleCount()
        {
            // arrange
            var expectedTotalVehicleCount = 7;

            SetupGetEuVehicleCount(2);
            SetupGetUsVehicleCount(5);

            // act
            var actual = await Testing.GetTotalVehicleCountAsync();

            // assert
            actual.Should().Be(expectedTotalVehicleCount);
        }

        [Theory]
        [InlineData(2, null, 2)]
        [InlineData(null, 2, 2)]
        [InlineData(null, null, 0)]
        public async Task ReturnTotalVehicleCount_GivenNullableValues(
            int? euVehicleCount,
            int? usVehicleCount,
            int expectedTotalVehicleCount)
        {
            // arrange
            SetupGetEuVehicleCount(euVehicleCount);
            SetupGetUsVehicleCount(usVehicleCount);

            // act
            var actual = await Testing.GetTotalVehicleCountAsync();

            // assert
            actual.Should().Be(expectedTotalVehicleCount);
        }
    }
}
