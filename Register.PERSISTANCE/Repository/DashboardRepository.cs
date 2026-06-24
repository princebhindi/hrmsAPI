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
            var today = DateTime.SpecifyKind(DateTime.UtcNow.Date, DateTimeKind.Utc);

            var combined = await _context.Employees
                .Take(1)
                .Select(_ => new
                {
                    TotalEmployees = _context.Employees.Count(x => x.IsActive),
                    TotalDepartments = _context.DepartMents.Count(x => x.IsActive),
                    PendingLeavesCount = _context.Leaves.Count(x => x.IsPending),
                    ActiveNoticesCount = _context.Notices.Count(x => x.IsActive && (x.ExpiryDate == null || x.ExpiryDate >= today)),
                    PresentCount = _context.Attendances.Count(x => x.Date.Date == today && x.Status == "Present")
                })
                .FirstOrDefaultAsync();

            if (combined != null)
            {
                stats.TotalEmployees = combined.TotalEmployees;
                stats.TotalDepartments = combined.TotalDepartments;
                stats.PendingLeavesCount = combined.PendingLeavesCount;
                stats.ActiveNoticesCount = combined.ActiveNoticesCount;
                stats.TodayAttendancePercentage = stats.TotalEmployees > 0
                    ? Math.Round(((double)combined.PresentCount / stats.TotalEmployees) * 100, 2)
                    : 0;
            }

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
