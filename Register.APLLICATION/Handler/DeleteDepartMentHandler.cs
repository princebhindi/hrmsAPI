using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Register.APPLICATION.Command;
using Register.APPLICATION.Interface;

namespace Register.APPLICATION.Handler
{
    public class DeleteDepartMentHandler : IRequestHandler<DeleteDepartMentCommand, APIResponse<bool>>
    {
        private readonly IDepartMent _repo;

        public DeleteDepartMentHandler(IDepartMent repo)
        {
            _repo = repo;
        }

        public async Task<APIResponse<bool>> Handle(DeleteDepartMentCommand request, CancellationToken cancellationToken)
        {
            var success = await _repo.DeleteDepartmentAsync(request.Id);

            return new APIResponse<bool>
            {
                Sucess = success,
                Message = success ? "Department deleted successfully" : "Department not found",
                Data = success
            };
        }
    }
}
