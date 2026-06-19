using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Register.APPLICATION.Command;
using Register.APPLICATION.Interface;

namespace Register.APPLICATION.Handler
{
    public class DeleteJobHandler : IRequestHandler<DeleteJobCommand, APIResponse<bool>>
    {
        private readonly IJob _repo;

        public DeleteJobHandler(IJob repo)
        {
            _repo = repo;
        }

        public async Task<APIResponse<bool>> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
        {
            var success = await _repo.DeleteJobAsync(request.Id);

            return new APIResponse<bool>
            {
                Sucess = success,
                Message = success ? "Job deleted successfully" : "Job not found",
                Data = success
            };
        }
    }
}
