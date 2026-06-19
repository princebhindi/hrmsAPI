using System;
using MediatR;
using Register.APPLICATION.DTO;

namespace Register.APPLICATION.Command
{
    public record AddDepartMentCommand(DepartMentDto DepartMent) : IRequest<APIResponse<DepartMentDto>>;
    public record UpdateDepartMentCommand(DepartMentDto DepartMent) : IRequest<APIResponse<bool>>;
    public record DeleteDepartMentCommand(Guid Id) : IRequest<APIResponse<bool>>;
}
