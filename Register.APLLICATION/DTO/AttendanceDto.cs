using System;
using Register.DOMAIN.Common;

namespace Register.APPLICATION.DTO
{
    public class AttendanceDto : Basic
    {
        public Guid? EmpId { get; set; }
        public DateTime Date { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public double? TotalHours { get; set; }
        public string? Status { get; set; }
    }
}
