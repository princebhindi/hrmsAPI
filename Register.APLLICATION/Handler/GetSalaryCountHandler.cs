using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;
using Register.APPLICATION.Queries;

namespace Register.APPLICATION.Handler
{
    public class GetSalaryCountHandler : IRequestHandler<GetSalaryCountQuery, APIResponse<int>>
    {
        private readonly ISalary _repo;

        public GetSalaryCountHandler(ISalary repo)
        {
            _repo = repo;
        }

        public async Task<APIResponse<int>> Handle(GetSalaryCountQuery request, CancellationToken cancellationToken)
        {
            var count = await _repo.GetSalaryCountAsync();

            return new APIResponse<int>
            {
                Sucess = true,
                Message = "Salary count retrieved successfully",
                Data = count
            };
        }
    }
}
