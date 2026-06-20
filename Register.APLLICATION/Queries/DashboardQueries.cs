using MediatR;
using Register.APPLICATION.DTO;

namespace Register.APPLICATION.Queries
{
    public record GetDashboardStatsQuery() : IRequest<APIResponse<DashboardStatsDto>>;
}
