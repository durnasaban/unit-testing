using Moq;
using UnitTestRefactoring.ExternalServices;

namespace UnitTestRefactoring.Tests.ReportingServiceTests
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

        protected void SetupGetEuVehicleCount(int? euVehicleCount) =>
            _euReportingServiceMock
                .Setup(eu => eu.GetVehicleCount())
                .ReturnsAsync(euVehicleCount);

        protected void SetupGetUsVehicleCount(int? usVehicleCount) =>
            _usReportingServiceMock
                .Setup(us => us.GetVehicleCount())
                .ReturnsAsync(usVehicleCount);

        protected  void VerifySetDiffVehicleCount(int expectedDiff) =>
        _mainReportingServiceMock
            .Verify(main => main.SetDiffVehicleCount(expectedDiff), Times.Once);
    }
}
