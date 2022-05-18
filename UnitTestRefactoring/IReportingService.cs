using System.Threading.Tasks;

namespace UnitTestRefactoring
{
    public interface IReportingService
    {
        Task<int> GetTotalVehicleCountAsync();

        Task SetDiffVehicleCountAsync();

        Task SetRatioOfUsToEuVehicleCountAsync();
    }
}
