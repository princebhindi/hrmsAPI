using System;
using Register.DOMAIN.Common;

namespace Register.APPLICATION.DTO
{
    public class SalaryDto : Basic
    {
        public Guid? EmpId { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal HRA { get; set; }
        public decimal Deductions { get; set; }
        public decimal InHandSalary { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
