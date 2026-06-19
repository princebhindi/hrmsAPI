using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;
using Register.APPLICATION.Queries;

namespace Register.APPLICATION.Handler
{
    public class GetNoticeByIdHandler : IRequestHandler<GetNoticeByIdQuery, APIResponse<NoticeResponseDto>>
    {
        private readonly INotice _repo;
        private readonly IMapper _mapper;

        public GetNoticeByIdHandler(INotice repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<APIResponse<NoticeResponseDto>> Handle(GetNoticeByIdQuery request, CancellationToken cancellationToken)
        {
            var notice = await _repo.GetNoticeByIdAsync(request.Id);
            if (notice == null)
            {
                return new APIResponse<NoticeResponseDto>
                {
                    Sucess = false,
                    Message = "Notice not found",
                    Data = null
                };
            }

            var dto = _mapper.Map<NoticeResponseDto>(notice);
            return new APIResponse<NoticeResponseDto>
            {
                Sucess = true,
                Message = "Notice retrieved successfully",
                Data = dto
            };
        }
    }
}
