using System;
using MediatR;
using Register.APPLICATION.DTO;

namespace Register.APPLICATION.Command
{
    public record AddEmployeeDocumentCommand(EmployeeDocumentDto EmployeeDocument) : IRequest<APIResponse<EmployeeDocumentDto>>;
    public record UpdateEmployeeDocumentCommand(EmployeeDocumentDto EmployeeDocument) : IRequest<APIResponse<bool>>;
    public record DeleteEmployeeDocumentCommand(Guid Id) : IRequest<APIResponse<bool>>;
}
