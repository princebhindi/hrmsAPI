using System;
using System.ComponentModel.DataAnnotations.Schema;
using Register.DOMAIN.Common;

namespace Register.DOMAIN.Entities
{
    public class Attendance : Basic
    {
        public Guid? EmpId { get; set; }

        [ForeignKey("EmpId")]
        public Employee? Employee { get; set; }

        public DateTime Date { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public double? TotalHours { get; set; }
        public string? Status { get; set; }
    }
}
