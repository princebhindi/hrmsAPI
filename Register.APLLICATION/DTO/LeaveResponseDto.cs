using System;
using Register.DOMAIN.Common;

namespace Register.APPLICATION.DTO
{
    public class LeaveResponseDto : Basic
    {
        public Guid? EmpId { get; set; }
        public EmployeeDto? Employee { get; set; }

        public Guid? UserId { get; set; }
        public UserRegisterDto? User { get; set; }

        public bool IsPending { get; set; }
        public bool IsRejected { get; set; }
        public bool IsApproved { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Reason { get; set; }
    }
}
