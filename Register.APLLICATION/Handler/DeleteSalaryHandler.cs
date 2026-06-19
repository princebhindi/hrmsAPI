using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Register.APPLICATION.Command;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;

namespace Register.APPLICATION.Handler
{
    public class DeleteSalaryHandler : IRequestHandler<DeleteSalaryCommand, APIResponse<bool>>
    {
        private readonly ISalary _repo;

        public DeleteSalaryHandler(ISalary repo)
        {
            _repo = repo;
        }

        public async Task<APIResponse<bool>> Handle(DeleteSalaryCommand request, CancellationToken cancellationToken)
        {
            var result = await _repo.DeleteSalaryAsync(request.Id);

            return new APIResponse<bool>
            {
                Sucess = result,
                Message = result ? "Salary deleted successfully" : "Salary not found",
                Data = result
            };
        }
    }
}
