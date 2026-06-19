using System;
using Register.DOMAIN.Common;

namespace Register.DOMAIN.Entities
{
    public class Job : Basic
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Guid? DeptId { get; set; }
        public DepartMent? Dept { get; set; }
    }
}
