using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Register.APPLICATION.Command;
using Register.APPLICATION.Interface;

namespace Register.APPLICATION.Handler
{
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, APIResponse<bool>>
    {
        private readonly IEmployee _repo;

        public DeleteEmployeeHandler(IEmployee repo)
        {
            _repo = repo;
        }

        public async Task<APIResponse<bool>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var success = await _repo.DeleteEmployeeAsync(request.Id);

            return new APIResponse<bool>
            {
                Sucess = success,
                Message = success ? "Employee deleted successfully" : "Employee not found",
                Data = success
            };
        }
    }
}
