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
    public class UpdateLeaveHandler : IRequestHandler<UpdateLeaveCommand, APIResponse<bool>>
    {
        private readonly ILeave _repo;
        private readonly IMapper _mapper;

        public UpdateLeaveHandler(ILeave repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<APIResponse<bool>> Handle(UpdateLeaveCommand request, CancellationToken cancellationToken)
        {
            var leave = _mapper.Map<Leave>(request.Leave);
            var result = await _repo.UpdateLeaveAsync(leave);

            return new APIResponse<bool>
            {
                Sucess = result,
                Message = result ? "Leave updated successfully" : "Leave not found",
                Data = result
            };
        }
    }
}
