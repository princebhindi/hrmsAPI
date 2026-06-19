using System;
using MediatR;
using Register.APPLICATION.DTO;

namespace Register.APPLICATION.Command
{
    public record AddAttendanceCommand(AttendanceDto Attendance) : IRequest<APIResponse<AttendanceDto>>;
    public record UpdateAttendanceCommand(AttendanceDto Attendance) : IRequest<APIResponse<bool>>;
    public record DeleteAttendanceCommand(Guid Id) : IRequest<APIResponse<bool>>;
}
