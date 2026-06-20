using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Register.APPLICATION.DTO;
using Register.APPLICATION.Interface;
using Register.PERSISTANCE.Context;

namespace Register.PERSISTANCE.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApllicationDbContext _context;

        public DashboardRepository(ApllicationDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardStatsDto> GetDashboardStatsAsync()
        {
            var stats = new DashboardStatsDto();

            // 1. Total headcount of active employees
            stats.TotalEmployees = await _context.Employees.CountAsync(x => x.IsActive);

            // 2. Total active departments
            stats.TotalDepartments = await _context.DepartMents.CountAsync(x => x.IsActive);

            // 3. Count of pending leaves
            stats.PendingLeavesCount = await _context.Leaves.CountAsync(x => x.IsPending);

            // 4. Count of active notices (not expired)
            var today = DateTime.Today;
            stats.ActiveNoticesCount = await _context.Notices.CountAsync(x => 
                x.IsActive && (x.ExpiryDate == null || x.ExpiryDate >= today));

            // 5. Today's present percentage
            if (stats.TotalEmployees > 0)
            {
                var presentCount = await _context.Attendances.CountAsync(x => 
                    x.Date.Date == today && x.Status == "Present");
                stats.TodayAttendancePercentage = Math.Round(((double)presentCount / stats.TotalEmployees) * 100, 2);
            }
            else
            {
                stats.TodayAttendancePercentage = 0;
            }

            // 6. Department wise headcounts
            stats.DepartmentHeadcounts = await _context.Employees
                .Where(x => x.IsActive && x.DeptId != null)
                .GroupBy(x => x.Dept!.Name)
                .Select(g => new DepartmentHeadcountDto
                {
                    DepartmentName = g.Key ?? "Unassigned",
                    EmployeeCount = g.Count()
                })
                .ToListAsync();

            return stats;
        }
    }
}
