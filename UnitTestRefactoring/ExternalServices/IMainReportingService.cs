using System.Threading.Tasks;
using Refit;

namespace UnitTestRefactoring.ExternalServices
{
    public interface IMainReportingService
    {
        [Post("/diff-vehicle-count/{vehicleCount}")]
        Task SetDiffVehicleCount(int vehicleCount);

        [Post("/ratio-eu-to-us-vehicle-count/{vehicleCount}")]
        Task SetRatioOfEuToUsVehicleCount(double vehicleCount);
    }
}
