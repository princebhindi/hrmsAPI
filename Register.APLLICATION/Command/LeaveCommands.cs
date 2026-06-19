using System;
using MediatR;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;

namespace Register.APPLICATION.Command
{
    public record AddLeaveCommand(LeavesDto Leave) : IRequest<APIResponse<LeavesDto>>, IInvalidateCache
    {
        public string[] InvalidateKeys => Array.Empty<string>();
        public string[] InvalidateVersions => new[] { "leaves_version" };
    }

    public record UpdateLeaveCommand(LeavesDto Leave) : IRequest<APIResponse<bool>>, IInvalidateCache
    {
        public string[] InvalidateKeys => new[] { $"leave_{Leave.Id}" };
        public string[] InvalidateVersions => new[] { "leaves_version" };
    }

    public record DeleteLeaveCommand(Guid Id) : IRequest<APIResponse<bool>>, IInvalidateCache
    {
        public string[] InvalidateKeys => new[] { $"leave_{Id}" };
        public string[] InvalidateVersions => new[] { "leaves_version" };
    }
}
