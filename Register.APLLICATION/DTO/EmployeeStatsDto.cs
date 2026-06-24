using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Register.DOMAIN.Entities;

namespace Register.APPLICATION.DTO
{
    public class EmployeeStatsDto
    {
        public Guid? EmpId { get; set; }
       public int? EmployeeLeaveCount { get; set; }
       public int? EmployeeTotalPresentDaysCount { get; set; }
       public int? EmployeeLateCheckinsCount { get; set; }
       
       public int? AverageWorkingHours { get; set; }

       public int? RemainingLeaves { get; set; }

       public int? PendingLeaveRequests { get; set; }

        public int? LastMonthsNetSalary { get; set; }

        public int? Deductions { get; set; }





    }
}
