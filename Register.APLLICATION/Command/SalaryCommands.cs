using System;
using MediatR;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;

namespace Register.APPLICATION.Command
{
    public record AddSalaryCommand(SalaryDto Salary) : IRequest<APIResponse<SalaryDto>>, IInvalidateCache
    {
        public string[] InvalidateKeys => Array.Empty<string>();
        public string[] InvalidateVersions => new[] { "salaries_version" };
    }

    public record UpdateSalaryCommand(SalaryDto Salary) : IRequest<APIResponse<bool>>, IInvalidateCache
    {
        public string[] InvalidateKeys => new[] { $"salary_{Salary.Id}" };
        public string[] InvalidateVersions => new[] { "salaries_version" };
    }

    public record DeleteSalaryCommand(Guid Id) : IRequest<APIResponse<bool>>, IInvalidateCache
    {
        public string[] InvalidateKeys => new[] { $"salary_{Id}" };
        public string[] InvalidateVersions => new[] { "salaries_version" };
    }
}
