using System;
using System.Collections.Generic;
using MediatR;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;

namespace Register.APPLICATION.Queries
{
    public record GetAllLeavesQuery(int PageNumber = 1, int PageSize = 10) : IRequest<APIResponse<IEnumerable<LeaveResponseDto>>>, ICachableQuery
    {
        public string CacheKey => $"leaves_all_{PageNumber}_{PageSize}";
        public string? VersionKey => "leaves_version";
        public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
    }

    public record GetLeaveByIdQuery(Guid Id) : IRequest<APIResponse<LeaveResponseDto>>, ICachableQuery
    {
        public string CacheKey => $"leave_{Id}";
        public string? VersionKey => null;
        public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
    }

    public record GetLeaveCountQuery() : IRequest<APIResponse<int>>;
}
