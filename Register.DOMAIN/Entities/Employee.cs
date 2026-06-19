using System;
using System.Collections.Generic;
using System.Text;
using Register.DOMAIN.Common;

namespace Register.DOMAIN.Entities
{
    public class Employee:Basic
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public Guid? DeptId { get; set; }
        public DepartMent? Dept { get; set; }
    }
}
