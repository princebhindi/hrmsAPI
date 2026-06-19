using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;
using Register.APPLICATION.Queries;

namespace Register.APPLICATION.Handler
{
    public class GetAllNoticesHandler : IRequestHandler<GetAllNoticesQuery, APIResponse<IEnumerable<NoticeResponseDto>>>
    {
        private readonly INotice _repo;
        private readonly IMapper _mapper;

        public GetAllNoticesHandler(INotice repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<APIResponse<IEnumerable<NoticeResponseDto>>> Handle(GetAllNoticesQuery request, CancellationToken cancellationToken)
        {
            var notices = await _repo.GetAllNoticesAsync(request.PageNumber, request.PageSize);
            var dtos = _mapper.Map<IEnumerable<NoticeResponseDto>>(notices);

            return new APIResponse<IEnumerable<NoticeResponseDto>>
            {
                Sucess = true,
                Message = "Notices retrieved successfully",
                Data = dtos
            };
        }
    }
}
