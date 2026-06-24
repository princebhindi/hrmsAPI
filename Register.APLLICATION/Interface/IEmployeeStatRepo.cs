using System;
using System.Collections.Generic;
using System.Text;
using Register.APPLICATION.DTO;

namespace Register.APPLICATION.Interface
{
    public interface IEmployeeStatRepo
    {
       public Task<EmployeeStatsDto> GetEmployeeStateData(Guid Empid);
    }
}
