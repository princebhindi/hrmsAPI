using System;
using Register.DOMAIN.Common;

namespace Register.APPLICATION.DTO
{
    public class EmployeeDocumentResponseDto : Basic
    {
        public Guid? EmpId { get; set; }
        public EmployeeDto? Employee { get; set; }

        public string? DocumentType { get; set; }
        public string? DocumentName { get; set; }
        public string? DocumentPath { get; set; }
    }
}
