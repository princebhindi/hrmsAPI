using System;
using Register.DOMAIN.Common;

namespace Register.DOMAIN.Entities
{
    public class Leave : Basic
    {
        public Guid? EmpId { get; set; }
        public Employee? Employee { get; set; }

        public Guid? UserId { get; set; }
        public UserRegister? User { get; set; }

        public bool IsPending { get; set; } = true;
        public bool IsRejected { get; set; } = false;
        public bool IsApproved { get; set; } = false;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Reason { get; set; }
    }
}
