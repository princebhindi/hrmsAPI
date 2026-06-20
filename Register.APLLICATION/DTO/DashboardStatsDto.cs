using System.Collections.Generic;

namespace Register.APPLICATION.DTO
{
    public class DashboardStatsDto
    {
        public int TotalEmployees { get; set; }
        public int TotalDepartments { get; set; }
        public int PendingLeavesCount { get; set; }
        public int ActiveNoticesCount { get; set; }
        public double TodayAttendancePercentage { get; set; }
        public List<DepartmentHeadcountDto> DepartmentHeadcounts { get; set; } = new List<DepartmentHeadcountDto>();
    }

    public class DepartmentHeadcountDto
    {
        public string DepartmentName { get; set; } = string.Empty;
        public int EmployeeCount { get; set; }
    }
}
