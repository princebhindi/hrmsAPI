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
    public class GetAttendanceByIdHandler : IRequestHandler<GetAttendanceByIdQuery, APIResponse<AttendanceResponseDto>>
    {
        private readonly IAttendance _repo;
        private readonly IMapper _mapper;

        public GetAttendanceByIdHandler(IAttendance repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<APIResponse<AttendanceResponseDto>> Handle(GetAttendanceByIdQuery request, CancellationToken cancellationToken)
        {
            var attendance = await _repo.GetAttendanceByIdAsync(request.Id);
            if (attendance == null)
            {
                return new APIResponse<AttendanceResponseDto>
                {
                    Sucess = false,
                    Message = "Attendance record not found",
                    Data = null
                };
            }

            var dto = _mapper.Map<AttendanceResponseDto>(attendance);
            return new APIResponse<AttendanceResponseDto>
            {
                Sucess = true,
                Message = "Attendance record retrieved successfully",
                Data = dto
            };
        }
    }
}
