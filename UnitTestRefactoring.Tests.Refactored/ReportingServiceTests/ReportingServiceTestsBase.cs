using System;
using Moq;
using UnitTestRefactoring.ExternalServices;

namespace UnitTestRefactoring.Tests.Refactored.ReportingServiceTests
{
    public class ReportingServiceTestsBase
    {
        protected readonly IReportingService Testing;

        private readonly Mock<IEuReportingService> _euReportingServiceMock;
        private readonly Mock<IUsReportingService> _usReportingServiceMock;
        private readonly Mock<IMainReportingService> _mainReportingServiceMock;

        protected ReportingServiceTestsBase()
        {
            _euReportingServiceMock = new Mock<IEuReportingService>();
            _usReportingServiceMock = new Mock<IUsReportingService>();
            _mainReportingServiceMock = new Mock<IMainReportingService>();

            Testing = new ReportingService(
                _euReportingServiceMock.Object,
                _usReportingServiceMock.Object,
                _mainReportingServiceMock.Object);
        }

        protected void SetupGetEuVehicleCount(int? vehicleCount) =>
            _euReportingServiceMock
                .Setup(eu => eu.GetVehicleCount())
                .ReturnsAsync(vehicleCount);

        protected void SetupGetUsVehicleCount(int? vehicleCount) =>
            _usReportingServiceMock
                .Setup(eu => eu.GetVehicleCount())
                .ReturnsAsync(vehicleCount);

        protected void VerifySettingDiffVehicleCountCalledOnceWith(int expectedDiff) =>
            _mainReportingServiceMock
                .Verify(main =>
                        main.SetDiffVehicleCount(expectedDiff),
                    Times.Once);
        protected void VerifySettingRatioOfEuToUsVehicleCountWith(Func<Times> times, double? ratio = null) =>
            _mainReportingServiceMock
                .Verify(main =>
                        main.SetRatioOfEuToUsVehicleCount(ratio ?? It.IsAny<double>()),
                    times);
    }
}
