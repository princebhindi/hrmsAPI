using System;
using Register.DOMAIN.Common;

namespace Register.DOMAIN.Entities
{
    public class Notice : Basic
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public Guid? TargetDepartmentId { get; set; }
        public DepartMent? TargetDepartment { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
