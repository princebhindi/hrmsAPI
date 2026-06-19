using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Register.APPLICATION.Command;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;
using Register.DOMAIN.Entities;

namespace Register.APPLICATION.Handler
{
    public class UpdateNoticeHandler : IRequestHandler<UpdateNoticeCommand, APIResponse<bool>>
    {
        private readonly INotice _repo;
        private readonly IMapper _mapper;

        public UpdateNoticeHandler(INotice repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<APIResponse<bool>> Handle(UpdateNoticeCommand request, CancellationToken cancellationToken)
        {
            var notice = _mapper.Map<Notice>(request.Notice);
            var result = await _repo.UpdateNoticeAsync(notice);

            return new APIResponse<bool>
            {
                Sucess = result,
                Message = result ? "Notice updated successfully" : "Notice not found",
                Data = result
            };
        }
    }
}
