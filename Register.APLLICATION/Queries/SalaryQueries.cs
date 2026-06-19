using System;
using System.Collections.Generic;
using MediatR;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;

namespace Register.APPLICATION.Queries
{
    public record GetAllSalariesQuery(int PageNumber = 1, int PageSize = 10) : IRequest<APIResponse<IEnumerable<SalaryResponseDto>>>, ICachableQuery
    {
        public string CacheKey => $"salaries_all_{PageNumber}_{PageSize}";
        public string? VersionKey => "salaries_version";
        public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
    }

    public record GetSalaryByIdQuery(Guid Id) : IRequest<APIResponse<SalaryResponseDto>>, ICachableQuery
    {
        public string CacheKey => $"salary_{Id}";
        public string? VersionKey => null;
        public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
    }

    public record GetSalaryCountQuery() : IRequest<APIResponse<int>>;
}
