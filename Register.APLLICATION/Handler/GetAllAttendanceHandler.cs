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
    public class GetAllAttendanceHandler : IRequestHandler<GetAllAttendanceQuery, APIResponse<IEnumerable<AttendanceResponseDto>>>
    {
        private readonly IAttendance _repo;
        private readonly IMapper _mapper;

        public GetAllAttendanceHandler(IAttendance repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<APIResponse<IEnumerable<AttendanceResponseDto>>> Handle(GetAllAttendanceQuery request, CancellationToken cancellationToken)
        {
            var attendanceList = await _repo.GetAllAttendanceAsync(request.PageNumber, request.PageSize);
            var dtos = _mapper.Map<IEnumerable<AttendanceResponseDto>>(attendanceList);

            return new APIResponse<IEnumerable<AttendanceResponseDto>>
            {
                Sucess = true,
                Message = "Attendance list retrieved successfully",
                Data = dtos
            };
        }
    }
}
