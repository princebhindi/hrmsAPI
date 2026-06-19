using System;
using System.Collections.Generic;
using MediatR;
using Register.APPLICATION.DTO;

namespace Register.APPLICATION.Queries
{
    public record GetAllDepartMentsQuery(int PageNumber = 1, int PageSize = 10) : IRequest<APIResponse<IEnumerable<DepartMentDto>>>;
    public record GetDepartMentByIdQuery(Guid Id) : IRequest<APIResponse<DepartMentDto>>;
}
