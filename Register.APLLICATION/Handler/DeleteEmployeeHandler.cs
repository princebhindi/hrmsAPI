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
        private readonly IUserRepo _userRepo;

        public DeleteEmployeeHandler(IEmployee repo, IUserRepo userRepo)
        {
            _repo = repo;
            _userRepo = userRepo;
        }

        public async Task<APIResponse<bool>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var success = await _repo.DeleteEmployeeAsync(request.Id);
            if (success)
            {
                await _userRepo.DeleteUserAsync(request.Id);
            }

            return new APIResponse<bool>
            {
                Sucess = success,
                Message = success ? "Employee deleted successfully" : "Employee not found",
                Data = success
            };
        }
    }
}
