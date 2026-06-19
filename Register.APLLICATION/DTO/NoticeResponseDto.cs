using System;
using Register.DOMAIN.Common;

namespace Register.APPLICATION.DTO
{
    public class NoticeResponseDto : Basic
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public Guid? TargetDepartmentId { get; set; }
        public DepartMentDto? TargetDepartment { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
