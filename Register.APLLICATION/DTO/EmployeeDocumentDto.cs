using System;
using Register.DOMAIN.Common;

namespace Register.APPLICATION.DTO
{
    public class EmployeeDocumentDto : Basic
    {
        public Guid? EmpId { get; set; }
        public string? DocumentType { get; set; }
        public string? DocumentName { get; set; }
        public string? DocumentPath { get; set; }
    }
}
