using System;
using Register.DOMAIN.Common;

namespace Register.APPLICATION.DTO
{
    public class NoticeDto : Basic
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public Guid? TargetDepartmentId { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
