using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;
using Register.APPLICATION.Queries;

namespace Register.APPLICATION.Handler
{
    public class GetAttendanceCountHandler : IRequestHandler<GetAttendanceCountQuery, APIResponse<int>>
    {
        private readonly IAttendance _repo;

        public GetAttendanceCountHandler(IAttendance repo)
        {
            _repo = repo;
        }

        public async Task<APIResponse<int>> Handle(GetAttendanceCountQuery request, CancellationToken cancellationToken)
        {
            var count = await _repo.GetAttendanceCountAsync();
            return new APIResponse<int>
            {
                Sucess = true,
                Message = "Attendance count retrieved successfully",
                Data = count
            };
        }
    }
}
