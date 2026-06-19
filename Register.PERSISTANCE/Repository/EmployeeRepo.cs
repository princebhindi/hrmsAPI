using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Register.APPLICATION.Interface;
using Register.DOMAIN.Entities;
using Register.PERSISTANCE.Context;

namespace Register.PERSISTANCE.Repository
{
    public class EmployeeRepo : IEmployee
    {
        private readonly ApllicationDbContext _context;

        public EmployeeRepo(ApllicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync(int pageNumber, int pageSize)
        {
            return await _context.Employees
                .Include(e => e.Dept)
                .Where(e => e.IsActive)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(Guid id)
        {
            return await _context.Employees
                .Include(e => e.Dept)
                .FirstOrDefaultAsync(e => e.Id == id && e.IsActive);
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            var existing = await _context.Employees.FindAsync(employee.Id);
            if (existing == null) return false;

            existing.Name = employee.Name;
            existing.LastName = employee.LastName;
            existing.Mobile = employee.Mobile;
            existing.Email = employee.Email;
            existing.DeptId = employee.DeptId;
            existing.OnUpdate = DateTime.Now;

            _context.Employees.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEmployeeAsync(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return false;

            employee.IsActive = false;
            employee.OnUpdate = DateTime.Now;
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetEmployeeCountAsync()
        {
            var count =await _context.Employees.CountAsync();
            return count;
        }
    }
}
