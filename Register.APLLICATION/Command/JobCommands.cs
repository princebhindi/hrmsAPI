using System;
using MediatR;
using Register.APPLICATION.DTO;

namespace Register.APPLICATION.Command
{
    public record AddJobCommand(JobDto Job) : IRequest<APIResponse<JobDto>>;
    public record UpdateJobCommand(JobDto Job) : IRequest<APIResponse<bool>>;
    public record DeleteJobCommand(Guid Id) : IRequest<APIResponse<bool>>;
}
