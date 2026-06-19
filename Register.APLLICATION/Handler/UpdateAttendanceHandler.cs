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
    public class UpdateAttendanceHandler : IRequestHandler<UpdateAttendanceCommand, APIResponse<bool>>
    {
        private readonly IAttendance _repo;
        private readonly IMapper _mapper;

        public UpdateAttendanceHandler(IAttendance repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<APIResponse<bool>> Handle(UpdateAttendanceCommand request, CancellationToken cancellationToken)
        {
            var attendance = _mapper.Map<Attendance>(request.Attendance);
            var result = await _repo.UpdateAttendanceAsync(attendance);

            return new APIResponse<bool>
            {
                Sucess = result,
                Message = result ? "Attendance updated successfully" : "Attendance record not found",
                Data = result
            };
        }
    }
}
