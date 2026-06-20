using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;
using Register.APPLICATION.Queries;

namespace Register.APPLICATION.Handler
{
    public class GetDashboardStatsHandler : IRequestHandler<GetDashboardStatsQuery, APIResponse<DashboardStatsDto>>
    {
        private readonly IDashboardRepository _repo;

        public GetDashboardStatsHandler(IDashboardRepository repo)
        {
            _repo = repo;
        }

        public async Task<APIResponse<DashboardStatsDto>> Handle(GetDashboardStatsQuery request, CancellationToken cancellationToken)
        {
            var stats = await _repo.GetDashboardStatsAsync();

            return new APIResponse<DashboardStatsDto>
            {
                Sucess = true,
                Message = "Dashboard statistics retrieved successfully",
                Data = stats
            };
        }
    }
}
