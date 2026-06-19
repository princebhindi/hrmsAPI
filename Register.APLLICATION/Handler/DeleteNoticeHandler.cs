using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Register.APPLICATION.Command;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;

namespace Register.APPLICATION.Handler
{
    public class DeleteNoticeHandler : IRequestHandler<DeleteNoticeCommand, APIResponse<bool>>
    {
        private readonly INotice _repo;

        public DeleteNoticeHandler(INotice repo)
        {
            _repo = repo;
        }

        public async Task<APIResponse<bool>> Handle(DeleteNoticeCommand request, CancellationToken cancellationToken)
        {
            var result = await _repo.DeleteNoticeAsync(request.Id);

            return new APIResponse<bool>
            {
                Sucess = result,
                Message = result ? "Notice deleted successfully" : "Notice not found",
                Data = result
            };
        }
    }
}
