using System;
using MediatR;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;

namespace Register.APPLICATION.Command
{
    public record AddEmployeeCommand(EmployeeDto Employee) : IRequest<APIResponse<EmployeeDto>>, IInvalidateCache
    {
        public string[] InvalidateKeys => Array.Empty<string>();
        public string[] InvalidateVersions => new[] { "employees_version" };
    }

    public record UpdateEmployeeCommand(EmployeeDto Employee) : IRequest<APIResponse<bool>>, IInvalidateCache
    {
        public string[] InvalidateKeys => new[] { $"employee_{Employee.Id}" };
        public string[] InvalidateVersions => new[] { "employees_version" };
    }

    public record DeleteEmployeeCommand(Guid Id) : IRequest<APIResponse<bool>>, IInvalidateCache
    {
        public string[] InvalidateKeys => new[] { $"employee_{Id}" };
        public string[] InvalidateVersions => new[] { "employees_version" };
    }
}
