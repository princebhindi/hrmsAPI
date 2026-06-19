using System;
using MediatR;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;

namespace Register.APPLICATION.Command
{
    public record AddNoticeCommand(NoticeDto Notice) : IRequest<APIResponse<NoticeDto>>, IInvalidateCache
    {
        public string[] InvalidateKeys => Array.Empty<string>();
        public string[] InvalidateVersions => new[] { "notices_version" };
    }

    public record UpdateNoticeCommand(NoticeDto Notice) : IRequest<APIResponse<bool>>, IInvalidateCache
    {
        public string[] InvalidateKeys => new[] { $"notice_{Notice.Id}" };
        public string[] InvalidateVersions => new[] { "notices_version" };
    }

    public record DeleteNoticeCommand(Guid Id) : IRequest<APIResponse<bool>>, IInvalidateCache
    {
        public string[] InvalidateKeys => new[] { $"notice_{Id}" };
        public string[] InvalidateVersions => new[] { "notices_version" };
    }
}
