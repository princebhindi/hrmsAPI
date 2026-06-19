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
    public class GetAllLeavesHandler : IRequestHandler<GetAllLeavesQuery, APIResponse<IEnumerable<LeaveResponseDto>>>
    {
        private readonly ILeave _repo;
        private readonly IMapper _mapper;

        public GetAllLeavesHandler(ILeave repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<APIResponse<IEnumerable<LeaveResponseDto>>> Handle(GetAllLeavesQuery request, CancellationToken cancellationToken)
        {
            var leaves = await _repo.GetAllLeavesAsync(request.PageNumber, request.PageSize);
            var dtos = _mapper.Map<IEnumerable<LeaveResponseDto>>(leaves);

            return new APIResponse<IEnumerable<LeaveResponseDto>>
            {
                Sucess = true,
                Message = "Leaves retrieved successfully",
                Data = dtos
            };
        }
    }
}
