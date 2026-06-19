using System;
using System.Collections.Generic;
using MediatR;
using Register.APPLICATION.DTO;

namespace Register.APPLICATION.Queries
{
    public record GetAllAttendanceQuery(int PageNumber = 1, int PageSize = 10) : IRequest<APIResponse<IEnumerable<AttendanceResponseDto>>>;
    public record GetAttendanceByIdQuery(Guid Id) : IRequest<APIResponse<AttendanceResponseDto>>;
    public record GetAttendanceCountQuery() : IRequest<APIResponse<int>>;
}
