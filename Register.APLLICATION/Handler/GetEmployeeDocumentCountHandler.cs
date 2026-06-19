using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;
using Register.APPLICATION.Queries;

namespace Register.APPLICATION.Handler
{
    public class GetEmployeeDocumentCountHandler : IRequestHandler<GetEmployeeDocumentCountQuery, APIResponse<int>>
    {
        private readonly IEmployeeDocument _repo;

        public GetEmployeeDocumentCountHandler(IEmployeeDocument repo)
        {
            _repo = repo;
        }

        public async Task<APIResponse<int>> Handle(GetEmployeeDocumentCountQuery request, CancellationToken cancellationToken)
        {
            var count = await _repo.GetEmployeeDocumentCountAsync();
            return new APIResponse<int>
            {
                Sucess = true,
                Message = "Employee document count retrieved successfully",
                Data = count
            };
        }
    }
}
