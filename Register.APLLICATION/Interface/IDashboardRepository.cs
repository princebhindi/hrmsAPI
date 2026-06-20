using System.Threading.Tasks;
using Register.APPLICATION.DTO;

namespace Register.APPLICATION.Interface
{
    public interface IDashboardRepository
    {
        Task<DashboardStatsDto> GetDashboardStatsAsync();
    }
}
