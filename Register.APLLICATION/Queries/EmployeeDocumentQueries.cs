using System;
using System.Collections.Generic;
using MediatR;
using Register.APPLICATION.DTO;

namespace Register.APPLICATION.Queries
{
    public record GetAllEmployeeDocumentsQuery(int PageNumber = 1, int PageSize = 10) : IRequest<APIResponse<IEnumerable<EmployeeDocumentResponseDto>>>;
    public record GetEmployeeDocumentByIdQuery(Guid Id) : IRequest<APIResponse<EmployeeDocumentResponseDto>>;
    public record GetEmployeeDocumentCountQuery() : IRequest<APIResponse<int>>;
}
