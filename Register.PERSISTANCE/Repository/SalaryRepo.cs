using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Register.APPLICATION.Interface;
using Register.DOMAIN.Entities;
using Register.PERSISTANCE.Context;

namespace Register.PERSISTANCE.Repository
{
    public class SalaryRepo : ISalary
    {
        private readonly ApllicationDbContext _context;

        public SalaryRepo(ApllicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Salary>> GetAllSalariesAsync(int pageNumber, int pageSize)
        {
            return await _context.Salaries
                .Include(s => s.Employee)
                .Where(s => s.IsActive)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Salary?> GetSalaryByIdAsync(Guid id)
        {
            return await _context.Salaries
                .Include(s => s.Employee)
                .FirstOrDefaultAsync(s => s.Id == id && s.IsActive);
        }

        public async Task<Salary> AddSalaryAsync(Salary salary)
        {
            if (salary.EmpId.HasValue)
            {
                salary.Deductions = await CalculateDeductionsAsync(salary.EmpId.Value, salary.BaseSalary, salary.Month, salary.Year);
            }
            salary.InHandSalary = Math.Max(0, salary.BaseSalary + salary.HRA - salary.Deductions);

            await _context.Salaries.AddAsync(salary);
            await _context.SaveChangesAsync();
            return salary;
        }

        public async Task<bool> UpdateSalaryAsync(Salary salary)
        {
            var existing = await _context.Salaries.FindAsync(salary.Id);
            if (existing == null) return false;

            existing.EmpId = salary.EmpId;
            existing.BaseSalary = salary.BaseSalary;
            existing.HRA = salary.HRA;
            existing.Month = salary.Month;
            existing.Year = salary.Year;

            if (salary.EmpId.HasValue)
            {
                existing.Deductions = await CalculateDeductionsAsync(salary.EmpId.Value, salary.BaseSalary, salary.Month, salary.Year);
            }
            else
            {
                existing.Deductions = 0;
            }
            existing.InHandSalary = Math.Max(0, existing.BaseSalary + existing.HRA - existing.Deductions);
            existing.OnUpdate = DateTime.Now;

            _context.Salaries.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSalaryAsync(Guid id)
        {
            var salary = await _context.Salaries.FindAsync(id);
            if (salary == null) return false;

            salary.IsActive = false;
            salary.OnUpdate = DateTime.Now;
            _context.Salaries.Update(salary);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetSalaryCountAsync()
        {
            return await _context.Salaries.CountAsync(s => s.IsActive);
        }

        private async Task<decimal> CalculateDeductionsAsync(Guid empId, decimal baseSalary, int month, int year)
        {
            var monthStart = new DateTime(year, month, 1);
            var monthEnd = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            // Get all approved active leaves overlapping with this month
            var leaves = await _context.Leaves
                .Where(l => l.EmpId == empId && l.IsApproved && l.IsActive &&
                            l.StartDate <= monthEnd && l.EndDate >= monthStart)
                .ToListAsync();

            int totalLeaveDays = 0;
            foreach (var leave in leaves)
            {
                var overlapStart = leave.StartDate < monthStart ? monthStart : leave.StartDate;
                var overlapEnd = leave.EndDate > monthEnd ? monthEnd : leave.EndDate;
                totalLeaveDays += (overlapEnd - overlapStart).Days + 1;
            }

            int daysInMonth = DateTime.DaysInMonth(year, month);
            decimal deductionPerDay = baseSalary / daysInMonth;
            return Math.Round(totalLeaveDays * deductionPerDay, 2);
        }
    }
}
