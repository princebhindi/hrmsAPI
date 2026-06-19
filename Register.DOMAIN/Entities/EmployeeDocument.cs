using System;
using Register.DOMAIN.Common;

namespace Register.DOMAIN.Entities
{
    public class EmployeeDocument : Basic
    {
        public Guid? EmpId { get; set; }
        public Employee? Employee { get; set; }

        public string? DocumentType { get; set; }
        public string? DocumentName { get; set; }
        public string? DocumentPath { get; set; }
    }
}
