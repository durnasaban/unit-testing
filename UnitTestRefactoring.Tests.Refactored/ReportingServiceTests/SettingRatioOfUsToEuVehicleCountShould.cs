using System.Threading.Tasks;
using Moq;
using Xunit;

namespace UnitTestRefactoring.Tests.Refactored.ReportingServiceTests
{
    public class SettingRatioOfUsToEuVehicleCountShould : ReportingServiceTestsBase
    {
        [Theory]
        [InlineData(10, 5, 2)]
        [InlineData(0, 5, 0)]
        public async Task SetRatio(
            int? euVehicleCount,
            int? usVehicleCount,
            double result)
        {
            // arrange
            SetupGetEuVehicleCount(euVehicleCount);
            SetupGetUsVehicleCount(usVehicleCount);

            // act
            await Testing.SetRatioOfUsToEuVehicleCountAsync();

            // assert
            VerifySettingRatioOfEuToUsVehicleCountWith(Times.Once, result);
        }

        [Fact]
        public async Task NotSetRatio_GivenNullUsVehicleCount()
        {
            // arrange
            SetupGetEuVehicleCount(5);
            SetupGetUsVehicleCount(null);

            // act
            await Testing.SetRatioOfUsToEuVehicleCountAsync();

            // assert
            VerifySettingRatioOfEuToUsVehicleCountWith(Times.Never);
        }
    }
}
