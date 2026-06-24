using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Register.APPLICATION.DTO;

namespace Register.APPLICATION.Queries
{
    public record GetEmployeePortalDataQueries(Guid EmpId) :IRequest<APIResponse<EmployeeStatsDto>>;
    
}
