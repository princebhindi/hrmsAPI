using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;
using Register.APPLICATION.Queries;

namespace Register.APPLICATION.Handler
{
    public class GetLeaveCountHandler : IRequestHandler<GetLeaveCountQuery, APIResponse<int>>
    {
        private readonly ILeave _repo;

        public GetLeaveCountHandler(ILeave repo)
        {
            _repo = repo;
        }

        public async Task<APIResponse<int>> Handle(GetLeaveCountQuery request, CancellationToken cancellationToken)
        {
            var count = await _repo.GetLeaveCountAsync();
            return new APIResponse<int>
            {
                Sucess = true,
                Message = "Leave count retrieved successfully",
                Data = count
            };
        }
    }
}
