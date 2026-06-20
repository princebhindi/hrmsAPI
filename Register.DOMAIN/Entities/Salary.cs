using System;
using System.ComponentModel.DataAnnotations.Schema;
using Register.DOMAIN.Common;

namespace Register.DOMAIN.Entities
{
    public class Salary : Basic
    {
        public Guid? EmpId { get; set; }

        [ForeignKey("EmpId")]
        public Employee? Employee { get; set; }

        public decimal BaseSalary { get; set; }
        public decimal HRA { get; set; }
        public decimal Deductions { get; set; }
        public decimal InHandSalary { get; set; }

        public int Month { get; set; }
        public int Year { get; set; }
    }
}
