using System.Threading.Tasks;
using Xunit;

namespace UnitTestRefactoring.Tests.Refactored.ReportingServiceTests
{
    public class SettingDiffsOfVehicleCountsShould : ReportingServiceTestsBase
    {
        [Theory]
        [InlineData(3, 2, 1)]
        [InlineData(2, 3, 1)]
        [InlineData(45, 2, 43)]
        [InlineData(2, 15, 13)]
        [InlineData(3, null, 3)]
        [InlineData(null, 3, 3)]
        [InlineData(null, null, 0)]
        public async Task SetPositiveDiffValue(
            int? euVehicleCount,
            int? usVehicleCount,
            int expectedDiff)
        {
            // arrange
            SetupGetEuVehicleCount(euVehicleCount);
            SetupGetUsVehicleCount(usVehicleCount);

            // act
            await Testing.SetDiffVehicleCountAsync();

            // assert
            VerifySettingDiffVehicleCountCalledOnceWith(expectedDiff);
        }
    }
}
