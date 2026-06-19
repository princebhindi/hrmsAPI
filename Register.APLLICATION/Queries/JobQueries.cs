using System;
using System.Collections.Generic;
using MediatR;
using Register.APPLICATION.DTO;

namespace Register.APPLICATION.Queries
{
    public record GetAllJobsQuery(int PageNumber = 1, int PageSize = 10) : IRequest<APIResponse<IEnumerable<JobDto>>>;
    public record GetJobByIdQuery(Guid Id) : IRequest<APIResponse<JobDto>>;
    public record GetJobCountQuery() : IRequest<APIResponse<int>>;
}
