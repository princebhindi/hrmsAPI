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
    public class AddNoticeHandler : IRequestHandler<AddNoticeCommand, APIResponse<NoticeDto>>
    {
        private readonly INotice _repo;
        private readonly IMapper _mapper;

        public AddNoticeHandler(INotice repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<APIResponse<NoticeDto>> Handle(AddNoticeCommand request, CancellationToken cancellationToken)
        {
            var notice = _mapper.Map<Notice>(request.Notice);
            var result = await _repo.AddNoticeAsync(notice);
            var resultDto = _mapper.Map<NoticeDto>(result);

            return new APIResponse<NoticeDto>
            {
                Sucess = true,
                Message = "Notice posted successfully",
                Data = resultDto
            };
        }
    }
}
