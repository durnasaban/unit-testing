using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using UnitTestRefactoring.ExternalServices;
using Xunit;

namespace UnitTestRefactoring.Tests
{
    public class ReportingServiceTests
    {
        private readonly IReportingService _testing;

        private readonly Mock<IEuReportingService> _euReportingServiceMock;
        private readonly Mock<IUsReportingService> _usReportingServiceMock;
        private readonly Mock<IMainReportingService> _mainReportingServiceMock;

        public ReportingServiceTests()
        {
            _euReportingServiceMock = new Mock<IEuReportingService>();
            _usReportingServiceMock = new Mock<IUsReportingService>();
            _mainReportingServiceMock = new Mock<IMainReportingService>();

            _testing = new ReportingService(
                _euReportingServiceMock.Object,
                _usReportingServiceMock.Object,
                _mainReportingServiceMock.Object);
        }

        #region Get Total Vehicles Count Tests 

        [Fact]
        public async Task GivenVehicleCountsOfEuAndUs_WhenGettingTotalVehicleCounts_ThenReturnTotalVehicleCount()
        {
            // arrange
            var euVehicleCount = 2;
            var usVehicleCount = 5;
            var expectedTotalVehicleCount = 7;

            _euReportingServiceMock
                .Setup(eu => eu.GetVehicleCount())
                .ReturnsAsync(euVehicleCount);

            _usReportingServiceMock
                .Setup(us => us.GetVehicleCount())
                .ReturnsAsync(usVehicleCount);

            // act
            var actual = await _testing.GetTotalVehicleCountAsync();

            // assert
            actual.Should().Be(expectedTotalVehicleCount);
        }

        [Theory]
        [InlineData(2, null, 2)]
        [InlineData(null, 2, 2)]
        [InlineData(null, null, 0)]
        public async Task GivenVehicleCountsOfEuAndUsWithNullValues_WhenGettingTotalVehicleCounts_ThenReturnTotalVehicleCount(
            int? euVehicleCount,
            int? usVehicleCount,
            int expectedTotalVehicleCount)
        {
            // arrange
            _euReportingServiceMock
                .Setup(eu => eu.GetVehicleCount())
                .ReturnsAsync(euVehicleCount);

            _usReportingServiceMock
                .Setup(us => us.GetVehicleCount())
                .ReturnsAsync(usVehicleCount);

            // act
            var actual = await _testing.GetTotalVehicleCountAsync();

            // assert
            actual.Should().Be(expectedTotalVehicleCount);
        }

        #endregion

        #region Set Diff of Vehicle Counts Tests 

        [Theory]
        [InlineData(3, 2, 1)]
        [InlineData(2, 3, 1)]
        [InlineData(45, 2, 43)]
        [InlineData(2, 15, 13)]
        [InlineData(3, null, 3)]
        [InlineData(null, 3, 3)]
        [InlineData(null, null, 0)]
        public async Task GivenNullableVehicleCountsOfEuAndUs_WhenSettingDiffsOfVehicleCounts_ThenPositiveDiffValueIsSet(
            int? euVehicleCount,
            int? usVehicleCount,
            int expectedDiff)
        {
            // arrange
            _euReportingServiceMock
                .Setup(eu => eu.GetVehicleCount())
                .ReturnsAsync(euVehicleCount);

            _usReportingServiceMock
                .Setup(us => us.GetVehicleCount())
                .ReturnsAsync(usVehicleCount);

            // act
            await _testing.SetDiffVehicleCountAsync();

            // assert
            _mainReportingServiceMock
                .Verify(main => main.SetDiffVehicleCount(expectedDiff), Times.Once);
        }

        #endregion

        #region Set the Ratio of Us to Eu Vehicle Counts

        [Theory]
        [InlineData(10, 5, 2)]
        [InlineData(0, 5, 0)]
        public async Task GivenEuAndUsVehicleCounts_WhenSettingRatio_ThenRatioValueIsSet(
            int? euVehicleCount,
            int? usVehicleCount,
            double result)
        {
            // arrange
            _euReportingServiceMock
                .Setup(eu => eu.GetVehicleCount())
                .ReturnsAsync(euVehicleCount);

            _usReportingServiceMock
                .Setup(us => us.GetVehicleCount())
                .ReturnsAsync(usVehicleCount);

            // act
            await _testing.SetRatioOfUsToEuVehicleCountAsync();

            // assert
            _mainReportingServiceMock
                .Verify(main => main.SetRatioOfEuToUsVehicleCount(result), Times.Once);
        }

        [Fact]
        public async Task GivenNullUsVehicleCount_WhenSettingRatio_ThenRatioValueIsNotSet()
        {
            // arrange
            int? euVehicleCount = 5;
            int? usVehicleCount = null;

            _euReportingServiceMock
                .Setup(eu => eu.GetVehicleCount())
                .ReturnsAsync(euVehicleCount);

            _usReportingServiceMock
                .Setup(us => us.GetVehicleCount())
                .ReturnsAsync(usVehicleCount);

            // act
            await _testing.SetRatioOfUsToEuVehicleCountAsync();

            // assert
            _mainReportingServiceMock
                .Verify(main => main.SetRatioOfEuToUsVehicleCount(It.IsAny<double>()), Times.Never);

        }

        #endregion
    }
}
