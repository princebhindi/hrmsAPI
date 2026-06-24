using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Register.APPLICATION;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;
using Register.PERSISTANCE.Context;

namespace Register.PERSISTANCE.Repository
{
    public class EmployeeStatRepo : IEmployeeStatRepo
    {
        private readonly ApllicationDbContext contex;

        public  EmployeeStatRepo (ApllicationDbContext _contex)
        {
            contex=_contex;
        }

        public async Task<EmployeeStatsDto> GetEmployeeStateData(Guid Empid)
        {
            var leavecount = await contex.Leaves.CountAsync(x => x.EmpId == Empid);
            var totalPresentDaysCount = await contex.Attendances.CountAsync(x => x.EmpId == Empid && x.Status == "Present");
            var lateCheckinsCount = await contex.Attendances.CountAsync(x => x.EmpId == Empid && x.Status == "Late");
            
            var hasAttendances = await contex.Attendances.AnyAsync(x => x.EmpId == Empid);
            int averageWorkingHours = 0;
            if (hasAttendances)
            {
                var avg = await contex.Attendances
                    .Where(x => x.EmpId == Empid)
                    .AverageAsync(a => EF.Functions.DateDiffHour(a.CheckInTime, a.CheckOutTime) ?? 0);
                averageWorkingHours = Convert.ToInt32(avg);
            }

            var pendingLeaveRequests = await contex.Leaves.CountAsync(x => x.EmpId == Empid && x.IsPending == true);
            var approvedLeaves = await contex.Leaves.CountAsync(x => x.EmpId == Empid && x.IsApproved == true);
            var remainingLeaves = Math.Max(0, 12 - approvedLeaves);

            var latestSalary = await contex.Salaries
                .Where(s => s.EmpId == Empid)
                .OrderByDescending(s => s.Year)
                .ThenByDescending(s => s.Month)
                .FirstOrDefaultAsync();

            int? lastMonthsNetSalary = latestSalary != null ? Convert.ToInt32(latestSalary.InHandSalary) : null;
            int? deductions = latestSalary != null ? Convert.ToInt32(latestSalary.Deductions) : null;

            return new EmployeeStatsDto
            {
                EmpId = Empid,
                EmployeeLeaveCount = leavecount,
                EmployeeTotalPresentDaysCount = totalPresentDaysCount,
                EmployeeLateCheckinsCount = lateCheckinsCount,
                AverageWorkingHours = averageWorkingHours,
                PendingLeaveRequests = pendingLeaveRequests,
                RemainingLeaves = remainingLeaves,
                LastMonthsNetSalary = lastMonthsNetSalary,
                Deductions = deductions
            };
        }
    }
}
