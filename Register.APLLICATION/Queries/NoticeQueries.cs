using System;
using System.Collections.Generic;
using MediatR;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;

namespace Register.APPLICATION.Queries
{
    public record GetAllNoticesQuery(int PageNumber = 1, int PageSize = 10) : IRequest<APIResponse<IEnumerable<NoticeResponseDto>>>, ICachableQuery
    {
        public string CacheKey => $"notices_all_{PageNumber}_{PageSize}";
        public string? VersionKey => "notices_version";
        public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
    }

    public record GetNoticeByIdQuery(Guid Id) : IRequest<APIResponse<NoticeResponseDto>>, ICachableQuery
    {
        public string CacheKey => $"notice_{Id}";
        public string? VersionKey => null;
        public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
    }

    public record GetNoticeCountQuery() : IRequest<APIResponse<int>>;
}
