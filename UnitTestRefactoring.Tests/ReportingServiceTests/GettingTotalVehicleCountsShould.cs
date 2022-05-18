using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using UnitTestRefactoring.ExternalServices;
using Xunit;

namespace UnitTestRefactoring.Tests.ReportingServiceTests
{
    public class GettingTotalVehicleCountsShould : ReportingServiceTestsBase
    {
        [Fact]
        public async Task ReturnTheTotalVehicleCount()
        {
            // arrange
            var euVehicleCount = 2;
            var usVehicleCount = 5;
            var expectedTotalVehicleCount = 7;

            SetupGetEuVehicleCount(euVehicleCount);
            SetupGetUsVehicleCount(usVehicleCount);

            // act
            var actual = await Testing.GetTotalVehicleCountAsync();

            // assert
            actual.Should().Be(expectedTotalVehicleCount);
        }

        [Theory]
        [InlineData(2, null, 2)]
        [InlineData(null, 2, 2)]
        [InlineData(null, null, 0)]
        public async Task ReturnTotalVehicleCount_GivenVehicleCountsOfEuAndUsWithNullValues(
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
