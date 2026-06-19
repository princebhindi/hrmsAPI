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
    public class AddLeaveHandler : IRequestHandler<AddLeaveCommand, APIResponse<LeavesDto>>
    {
        private readonly ILeave _repo;
        private readonly IMapper _mapper;

        public AddLeaveHandler(ILeave repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<APIResponse<LeavesDto>> Handle(AddLeaveCommand request, CancellationToken cancellationToken)
        {
            var leave = _mapper.Map<Leave>(request.Leave);
            var result = await _repo.AddLeaveAsync(leave);
            var resultDto = _mapper.Map<LeavesDto>(result);

            return new APIResponse<LeavesDto>
            {
                Sucess = true,
                Message = "Leave added successfully",
                Data = resultDto
            };
        }
    }
}
