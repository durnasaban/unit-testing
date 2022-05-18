using System.Threading.Tasks;
using Refit;

namespace UnitTestRefactoring.ExternalServices
{
    public interface IUsReportingService
    {
        [Get("/vehicle-count")]
        Task<int?> GetVehicleCount();
    }
}
