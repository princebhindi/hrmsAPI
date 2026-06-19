using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Register.APPLICATION.Interface;
using Register.APPLICATION.Queries;

namespace Register.APPLICATION.Handler
{
    public class GetEmployeeCountHandler : IRequestHandler<GetEmployeeCountQuery, APIResponse<int>>
    {
        private readonly IEmployee _repo;

        public GetEmployeeCountHandler(IEmployee repo)
        {
            _repo = repo;
        }

        public async Task<APIResponse<int>> Handle(GetEmployeeCountQuery request, CancellationToken cancellationToken)
        {
            var count = await _repo.GetEmployeeCountAsync();

            return new APIResponse<int>
            {
                Sucess = true,
                Message = "Employee count retrieved successfully",
                Data = count
            };
        }
    }
}
