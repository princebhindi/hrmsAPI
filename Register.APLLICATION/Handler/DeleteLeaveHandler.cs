using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Register.APPLICATION.Command;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;

namespace Register.APPLICATION.Handler
{
    public class DeleteLeaveHandler : IRequestHandler<DeleteLeaveCommand, APIResponse<bool>>
    {
        private readonly ILeave _repo;

        public DeleteLeaveHandler(ILeave repo)
        {
            _repo = repo;
        }

        public async Task<APIResponse<bool>> Handle(DeleteLeaveCommand request, CancellationToken cancellationToken)
        {
            var result = await _repo.DeleteLeaveAsync(request.Id);

            return new APIResponse<bool>
            {
                Sucess = result,
                Message = result ? "Leave deleted successfully" : "Leave not found",
                Data = result
            };
        }
    }
}
