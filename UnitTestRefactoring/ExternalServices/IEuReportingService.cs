using System.Threading.Tasks;
using Refit;

namespace UnitTestRefactoring.ExternalServices
{
    public interface IEuReportingService
    {
        [Get("/vehicle-count")]
        Task<int?> GetVehicleCount();
    }
}
