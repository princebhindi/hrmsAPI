using System;
using System.Collections.Generic;
using MediatR;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;

namespace Register.APPLICATION.Queries
{
    public record GetAllEmployeesQuery(int PageNumber = 1, int PageSize = 10) : IRequest<APIResponse<IEnumerable<EmployeeDto>>>, ICachableQuery
    {
        public string CacheKey => $"employees_all_{PageNumber}_{PageSize}";
        public string? VersionKey => "employees_version";
        public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
    }

    public record GetEmployeeByIdQuery(Guid Id) : IRequest<APIResponse<EmployeeDto>>, ICachableQuery
    {
        public string CacheKey => $"employee_{Id}";
        public string? VersionKey => null;
        public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
    }

    public record GetEmployeeCountQuery() : IRequest<APIResponse<int>>;
}
