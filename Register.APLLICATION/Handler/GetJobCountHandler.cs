using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Register.APPLICATION.Interface;
using Register.APPLICATION.Queries;

namespace Register.APPLICATION.Handler
{
    public class GetJobCountHandler : IRequestHandler<GetJobCountQuery, APIResponse<int>>
    {
        private readonly IJob _repo;

        public GetJobCountHandler(IJob repo)
        {
            _repo = repo;
        }

        public async Task<APIResponse<int>> Handle(GetJobCountQuery request, CancellationToken cancellationToken)
        {
            var count = await _repo.GetJobCountAsync();

            return new APIResponse<int>
            {
                Sucess = true,
                Message = "Job count retrieved successfully",
                Data = count
            };
        }
    }
}
