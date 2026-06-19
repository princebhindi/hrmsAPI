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
    public class GetLeaveByIdHandler : IRequestHandler<GetLeaveByIdQuery, APIResponse<LeaveResponseDto>>
    {
        private readonly ILeave _repo;
        private readonly IMapper _mapper;

        public GetLeaveByIdHandler(ILeave repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<APIResponse<LeaveResponseDto>> Handle(GetLeaveByIdQuery request, CancellationToken cancellationToken)
        {
            var leave = await _repo.GetLeaveByIdAsync(request.Id);
            if (leave == null)
            {
                return new APIResponse<LeaveResponseDto>
                {
                    Sucess = false,
                    Message = "Leave not found",
                    Data = null
                };
            }

            var dto = _mapper.Map<LeaveResponseDto>(leave);
            return new APIResponse<LeaveResponseDto>
            {
                Sucess = true,
                Message = "Leave retrieved successfully",
                Data = dto
            };
        }
    }
}
