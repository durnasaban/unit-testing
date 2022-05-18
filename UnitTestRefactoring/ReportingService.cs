using System;
using System.Threading.Tasks;
using UnitTestRefactoring.ExternalServices;

namespace UnitTestRefactoring
{
    public class ReportingService : IReportingService
    {
        private readonly IEuReportingService _euReportingService;
        private readonly IUsReportingService _usReportingService;
        private readonly IMainReportingService _mainReportingService;

        public ReportingService(
            IEuReportingService euReportingService, 
            IUsReportingService usReportingService, 
            IMainReportingService mainReportingService)
        {
            _euReportingService = euReportingService;
            _usReportingService = usReportingService;
            _mainReportingService = mainReportingService;
        }

        public async Task<int> GetTotalVehicleCountAsync()
        {
            var (euVehicleCount, usVehicleCount) = await GetEuAndUsVehicleCountsAsync();

            return (euVehicleCount ?? 0) + (usVehicleCount ?? 0);
        }

        public async Task SetDiffVehicleCountAsync()
        {
            var (euVehicleCount, usVehicleCount) = await GetEuAndUsVehicleCountsAsync();
            
            var diffValue = (euVehicleCount ?? 0) - (usVehicleCount ?? 0);

            var positiveDiffValue = Math.Abs(diffValue);

            await _mainReportingService.SetDiffVehicleCount(positiveDiffValue);
        }

        public async Task SetRatioOfUsToEuVehicleCountAsync()
        {
            var (euVehicleCount, usVehicleCount) = await GetEuAndUsVehicleCountsAsync();

            if (!usVehicleCount.HasValue)
            {
                return;
            }

            var ratio = (euVehicleCount ?? 0) / (double)usVehicleCount.Value;

            await _mainReportingService.SetRatioOfEuToUsVehicleCount(ratio);
        }

        private async Task<(int? euVehicleCount, int? usVehicleCount)> GetEuAndUsVehicleCountsAsync()
        {
            var euVehicleCount = _euReportingService.GetVehicleCount();
            var usVehicleCount = _usReportingService.GetVehicleCount();

            await Task.WhenAll(euVehicleCount, usVehicleCount);

            return (euVehicleCount.Result, usVehicleCount.Result);
        }
    }
}