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
    public class AddAttendanceHandler : IRequestHandler<AddAttendanceCommand, APIResponse<AttendanceDto>>
    {
        private readonly IAttendance _repo;
        private readonly IMapper _mapper;

        public AddAttendanceHandler(IAttendance repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<APIResponse<AttendanceDto>> Handle(AddAttendanceCommand request, CancellationToken cancellationToken)
        {
            var attendance = _mapper.Map<Attendance>(request.Attendance);
            var result = await _repo.AddAttendanceAsync(attendance);
            var resultDto = _mapper.Map<AttendanceDto>(result);

            return new APIResponse<AttendanceDto>
            {
                Sucess = true,
                Message = "Attendance recorded successfully",
                Data = resultDto
            };
        }
    }
}
